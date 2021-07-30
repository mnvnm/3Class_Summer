#include "SimpleAudioEngine.h"
#include "MogiMain.h"
#include "BodyChoose.h"
#include "mogi.h"
#include "StageC.h"
#include "mogiBoss.h"
#include "Blooding.h"
#include "AudioEngine.h"

#pragma execution_character_set("utf-8")

USING_NS_CC;
using namespace cocos2d::experimental;

Scene* MogiMain::createScene()
{
	auto scene	=	Scene::createWithPhysics(); // 물리에서 중요 //

	auto layer	= MogiMain::create();

	scene->addChild(layer);
	//물리적 중력 설정및 디버그 Draw
	scene->getPhysicsWorld()->setGravity(Vec2(0, -1000.0f));//물리적 중력
	scene->getPhysicsWorld()->setDebugDrawMask(PhysicsWorld::DEBUGDRAW_ALL);//디버그 Draw

	return scene;
}

bool MogiMain::init()
{
	if (!LayerColor::initWithColor(Color4B::WHITE))
	{
		return false;
	}
	Mt = 0;

	this->scheduleUpdate();

	this->setKeyboardEnabled(true);
	////////////////////////////마우스 이벤트 /////////////////////////

	/*EventListenerMouse* mouse = EventListenerMouse::create();
	mouse->onMouseMove = CC_CALLBACK_0(Mogi::onMouseMove, this); 
	_eventDispatcher->addEventListenerWithSceneGraphPriority(mouse, this);*/

	///////////////////////////////충돌 이벤트 /////////////////////////////
	/*auto contactListener = EventListenerPhysicsContact::create();
	contactListener->onContactBegin = CC_CALLBACK_1(MogiMain::onContactBegin, this);
	_eventDispatcher->addEventListenerWithSceneGraphPriority(contactListener, this);*/

	AudioEngine::preload("mogi/sound/Main BGM.mp3");
	AudioEngine::preload("mogi/sound/switch-24.mp3");

	Main = Sprite::create("mogi/main.png");
	Main->setPosition(Vec2::ZERO);
	Main->setAnchorPoint(Vec2::ZERO);

	this->addChild(Main);

	MenuItemImage* StartItem = MenuItemImage::create("mogi/1.png", "mogi/2.png", CC_CALLBACK_1(MogiMain::menuCloseCallback, this));
	StartItem->setPosition(Vec2(-450, -120));
	StartItem->setAnchorPoint(Vec2::ZERO);

	auto menu = Menu::create(StartItem,NULL);

	AudioEngine::stopAll();
	MogiMain_bg = AudioEngine::play2d("mogi/sound/Main BGM.mp3", true);//뒤에 true가 붙으면 무한재생
	AudioEngine::setVolume(MogiMain_bg, MogiMain_Sound_Scale);

	this->addChild(menu ,3);


}

void MogiMain::menuCloseCallback(Ref* pSender)
{
	Stage_choose = true;
	button = AudioEngine::play2d("mogi/sound/switch-24.mp3");
	Director::getInstance()->replaceScene(TransitionFade::create(2.0f,StageC::createScene(),Color3B(0,0,0)));
}


 void MogiMain::update(float delta)
 {
	 if (MogiMain_Sound_Scale < 0.5)
	 {
		 MogiMain_Sound_Scale += 0.0025; 
	 }
	 if (Stage_choose)
	 {
		 MogiMain_Sound_Scale -= 0.25;
	 }
	 AudioEngine::setVolume(MogiMain_bg, MogiMain_Sound_Scale);
 }
