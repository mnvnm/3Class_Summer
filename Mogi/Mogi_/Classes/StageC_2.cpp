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

Scene* StageC_2::createScene()
{
	auto scene = Scene::createWithPhysics(); // 물리에서 중요 //

	auto layer = StageC_2::create();

	scene->addChild(layer);
	//물리적 중력 설정및 디버그 Draw
	scene->getPhysicsWorld()->setGravity(Vec2(0, -1000.0f));//물리적 중력
	scene->getPhysicsWorld()->setDebugDrawMask(PhysicsWorld::DEBUGDRAW_ALL);//디버그 Draw

	return scene;
}

bool StageC_2::init()
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

	Sprite* Main = Sprite::create("mogi/ㅂ배경.png");
	Main->setPosition(Vec2::ZERO);
	Main->setAnchorPoint(Vec2::ZERO);
	Main->setContentSize(Size(1280, 720));

	this->addChild(Main);

	//auto Stg1 = MenuItemImage::create("mogi/Play_button.png", "mogi/Play_button.png", CC_CALLBACK_1(StageC::menuCloseCallback, this));
	auto bt_sp = Sprite::create("mogi/잠긴 플레이버튼.png");
	bt_sp->setColor(Color3B(170, 170, 170));
	Stg2 = MenuItemSprite::create(Sprite::create("mogi/잠긴 플레이버튼.png"), bt_sp);
	Stg2->setScale(1);
	Stg2->setPosition(Vec2(1280 / 2 + 25, 30));

	auto Name = Sprite::create("mogi/잠긴 이름표.png");
	Name->setPosition(Vec2(1280, 720) / 2);
	this->addChild(Name);

	auto Lock = Sprite::create("mogi/자물쇠.png");
	Lock->setPosition(Vec2(1280, 200) / 2);
	Lock->setScale(0.5f);
	this->addChild(Lock, 10);

	auto Lock_2 = Sprite::create("mogi/자물쇠.png");
	Lock_2->setPosition(Vec2(1280 / 2 + 350, 480));
	Lock_2->setScale(0.5f);
	this->addChild(Lock_2, 10);

	auto left_Next_button_2 = MenuItemImage::create("mogi/Next_button.png", "mogi/Next_button_h.png", CC_CALLBACK_1(StageC_2::Stage2, this));
	left_Next_button_2->setScale(1);
	left_Next_button_2->setAnchorPoint(Vec2::ZERO);
	left_Next_button_2->setPosition(Vec2(45, 320));

	auto right_Next_button_2 = MenuItemImage::create("mogi/Next_button_r.png", "mogi/Next_button_r_h.png", CC_CALLBACK_1(StageC_2::Stage2, this));
	right_Next_button_2->setPosition(Vec2(1280 - 45, 320));
	right_Next_button_2->setAnchorPoint(Vec2::ZERO);

	auto menu = Menu::create(Stg2, left_Next_button_2, right_Next_button_2, NULL);
	menu->setAnchorPoint(Vec2::ZERO);
	menu->setPosition(Vec2(-32, 70));

	this->addChild(menu, 1);
}

void StageC_2::menuCloseCallback(Ref* pSender)
{
	Director::getInstance()->replaceScene(TransitionFade::create(2.0f, Mogi::createScene(), Color3B(255, 255, 255)));
}


void StageC_2::Stage2(Ref* pSender)
{
	Director::getInstance()->replaceScene(TransitionFade::create(0, StageC::createScene()));
}

//void MogiMain::update(float delta)
//{
   //	 
//}
