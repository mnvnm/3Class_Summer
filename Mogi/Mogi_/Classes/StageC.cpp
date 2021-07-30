#include "SimpleAudioEngine.h"
#include "MogiMain.h"
#include "BodyChoose.h"
#include "mogi.h"
#include "StageC.h"
#include "mogiBoss.h"
#include "Blooding.h"
#include "StageC_2.h"
#include "AudioEngine.h"

#pragma execution_character_set("utf-8")

USING_NS_CC;
using namespace cocos2d::experimental;

Scene* StageC::createScene()
{
	auto scene	=	Scene::createWithPhysics(); // 물리에서 중요 //

	auto layer	= StageC::create();

	scene->addChild(layer);
	//물리적 중력 설정및 디버그 Draw
	scene->getPhysicsWorld()->setGravity(Vec2(0, -1000.0f));//물리적 중력
	scene->getPhysicsWorld()->setDebugDrawMask(PhysicsWorld::DEBUGDRAW_ALL);//디버그 Draw

	return scene;
}

bool StageC::init()
{
	if (!LayerColor::initWithColor(Color4B::WHITE))
	{
		return false;
	}
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
	AudioEngine::preload("mogi/sound/작은전등스위치.mp3");
	AudioEngine::preload("mogi/sound/switch-24.mp3");

	Sprite* Main = Sprite::create("mogi/StageC_Bg.png");
	Main->setPosition(Vec2::ZERO);
	Main->setAnchorPoint(Vec2::ZERO);
	Main->setContentSize(Size(1280, 720));

	this->addChild(Main);

	//auto Stg1 = MenuItemImage::create("mogi/Play_button.png", "mogi/Play_button.png", CC_CALLBACK_1(StageC::menuCloseCallback, this));
	auto bt_sp = Sprite::create("mogi/Play_button.png");
	bt_sp->setColor(Color3B(170, 170, 170));
	Stg = MenuItemSprite::create(Sprite::create("mogi/Play_button.png"), bt_sp, CC_CALLBACK_1(StageC::menuCloseCallback, this));
	Stg->setScale(0.52f);
	Stg->setPosition(Vec2(1280 / 2 + 25, 30));

	left_Next_button = MenuItemImage::create("mogi/Next_button.png", "mogi/Next_button_h.png", CC_CALLBACK_1(StageC::Stage1, this));
	left_Next_button->setScale(1);
	left_Next_button->setAnchorPoint(Vec2::ZERO);
	left_Next_button->setPosition(Vec2(45, 320));

	right_Next_button = MenuItemImage::create("mogi/Next_button_r.png", "mogi/Next_button_r_h.png", CC_CALLBACK_1(StageC::Stage2, this));
	right_Next_button->setPosition(Vec2(1280 - 45, 320));
	right_Next_button->setAnchorPoint(Vec2::ZERO);

	auto menu = Menu::create(Stg, NULL);
	menu->setAnchorPoint(Vec2::ZERO);
	menu->setPosition(Vec2(-32, 70));

	auto menu3 = Menu::create(left_Next_button, right_Next_button, NULL);
	menu3->setAnchorPoint(Vec2::ZERO);
	menu3->setPosition(Vec2(-32, 70));

	this->addChild(menu);
	this->addChild(menu3 , 100);

	Main2 = Sprite::create("mogi/ㅂ배경.png");
	Main2->setPosition(Vec2::ZERO);
	Main2->setAnchorPoint(Vec2::ZERO);
	Main2->setContentSize(Size(1280, 720));

	Main2->setVisible(false);


	this->addChild(Main2);

	auto bt_sp2 = Sprite::create("mogi/잠긴 플레이버튼.png");
	bt_sp2->setColor(Color3B(170, 170, 170));
	Stg2 = MenuItemSprite::create(Sprite::create("mogi/잠긴 플레이버튼.png"), bt_sp2);
	Stg2->setScale(1);
	Stg2->setPosition(Vec2(1280 / 2 + 25, 30));

	auto Name = Sprite::create("mogi/잠긴 이름표.png");
	Name->setPosition(Vec2(1280, 720) / 2);
	Main2->addChild(Name);

	auto Lock = Sprite::create("mogi/자물쇠.png");
	Lock->setPosition(Vec2(640, 100));
	Lock->setScale(0.5f);
	Main2->addChild(Lock, 10);

	auto Lock_2 = Sprite::create("mogi/자물쇠.png");
	Lock_2->setPosition(Vec2(1280 / 2 + 350, 480));
	Lock_2->setScale(0.5f);
	Name->addChild(Lock_2);

	auto menu2 = Menu::create(Stg2, NULL);
	menu2->setAnchorPoint(Vec2::ZERO);
	menu2->setPosition(Vec2(-32, 70));

	Main2->addChild(menu2);

}

void StageC::menuCloseCallback(Ref* pSender)
{
	Play_sound = AudioEngine::play2d("mogi/sound/switch-24.mp3");
	Stage_choose = true;
	Director::getInstance()->replaceScene(TransitionFade::create(2.0f, Mogi::createScene(),Color3B(0, 0, 0)));
}


void StageC::Stage1(Ref* pSender)
{
	 Stage_Next = AudioEngine::play2d("mogi/sound/작은전등스위치.mp3");
	 log("false");
	 Main2->setVisible(false);
}	 

void StageC::Stage2(Ref* pSender)
{
	 Stage_Next = AudioEngine::play2d("mogi/sound/작은전등스위치.mp3");
	 
	 log("true");
	 Main2->setVisible(true);
}

 void StageC::update(float delta)
 {
	 if (Stage_choose == true)
	 {
		 if (MogiStage_Sound_Scale >= 0.5)
		 {
		 MogiStage_Sound_Scale -= 0.0025;

		 }
	 }

	 if (Stage_choose == false)
	 {
		 if (MogiStage_Sound_Scale < 0.5)
		 {
		 MogiStage_Sound_Scale += 0.0025;
		 }

	 }
	 AudioEngine::setVolume(MogiStage_Sound, MogiStage_Sound_Scale);
 }
