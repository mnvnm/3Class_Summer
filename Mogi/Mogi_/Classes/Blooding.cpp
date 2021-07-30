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

 Scene* Blood::createScene()
 {
	 auto scene = Scene::createWithPhysics(); // 물리에서 중요 //
	 auto layer = Blood::create();

	 scene->addChild(layer);
	 //물리적 중력 설정및 디버그 Draw
	 scene->getPhysicsWorld()->setGravity(Vec2(0, -1000.0f));//물리적 중력
	 scene->getPhysicsWorld()->setDebugDrawMask(PhysicsWorld::DEBUGDRAW_ALL);//디버그 Draw

	 return scene;


 }

 bool Blood::init()
 {

	 if (!LayerColor::initWithColor(Color4B::WHITE))
	 {
		 return false;
	 }
	 this->scheduleUpdate();
	 Rotate_speed = 360;

	 this->setKeyboardEnabled(true);
	 //////////////////////////////마우스 이벤트 /////////////////////////

	 EventListenerMouse* mouse = EventListenerMouse::create();
	 mouse->onMouseMove = CC_CALLBACK_1(Blood::onMouseMove, this);
	 _eventDispatcher->addEventListenerWithSceneGraphPriority(mouse, this);

	 ///////////////////////////////충돌 이벤트 /////////////////////////////
	 //auto contactListener = EventListenerPhysicsContact::create();
	 //contactListener->onContactBegin = CC_CALLBACK_1(MogiM::onContactBegin, this);
	 //_eventDispatcher->addEventListenerWithSceneGraphPriority(contactListener, this);

	 AudioEngine::preload("mogi/sound/에엥.mp3");
	 AudioEngine::preload("mogi/sound/띠리링.mp3");
	 AudioEngine::preload("mogi/sound/띠링.mp3");

	 speed = 0;
	 Check = false;
	 d = new MosCircle();
	 d->setPosition(Vec2(1280 - 300, 300));
	 this->addChild(d, 999999);

	 d->setUserRotation(random(0, 360));
	 d->setUserAngle(random(45, 90));

	 Bg1 = Sprite::create("mogi/Blood_Bg.png");
	 Bg1->setContentSize(Size(1280, 720));
	 Bg1->setPosition(Vec2::ZERO);
	 Bg1->setAnchorPoint(Vec2::ZERO);
	 this->addChild(Bg1);

	 Bg2 = Sprite::create("mogi/Blood_Bg_2.png");
	 Bg2->setContentSize(Size(1280, 720));
	 Bg2->setPosition(Vec2::ZERO);
	 Bg2->setAnchorPoint(Vec2::ZERO);
	 this->addChild(Bg2);
	 Bg2->setVisible(false);

	 Bg2->runAction(RepeatForever::create(
		 Sequence::create(
			 CallFunc::create([=]() {Bg2->setVisible(true); }), DelayTime::create(0.5f),
			 CallFunc::create([=]() {Bg2->setVisible(false); }), DelayTime::create(0.5f), nullptr)));

	 CheckBar = Sprite::create("mogi/Check.png");
	 CheckBar->setAnchorPoint(Vec2(0.50695f, 0.4575));
	 CheckBar->setPosition(Vec2(1280 - 300, 300));
	 CheckBar->setScale(1.45);
	 this->addChild(CheckBar);

	 angryBar_Bg = Sprite::create("mogi/angry.png");
	 angryBar_Bg->setAnchorPoint(Vec2(0, 1));
	 angryBar_Bg->setPosition(Vec2(50, 720 - 20));
	 this->addChild(angryBar_Bg);

	 angryBar = Sprite::create("mogi/angryBar.png");
	 angryBar->setAnchorPoint(Vec2(0, 1));
	 angryBar->setPosition(Vec2(202, 720 - 69));
	 angryBar->setScaleX(0);
	 this->addChild(angryBar);

	 bloodBar_Bg = Sprite::create("mogi/blood.png");
	 bloodBar_Bg->setAnchorPoint(Vec2(0, 1));
	 bloodBar_Bg->setPosition(Vec2(50, 520));
	 this->addChild(bloodBar_Bg);

	 bloodBar = Sprite::create("mogi/BloodBar.png");
	 bloodBar->setAnchorPoint(Vec2(0, 1));
	 bloodBar->setPosition(Vec2(202, 471 + 10));
	 bloodBar->setScaleX(0);
	 this->addChild(bloodBar);
	// bloodBar_Bg = Sprite::create("mogi/blood.png");

	 f_body[0] = Configuration::getInstance()->getValue("Body0").asInt();//머리
	 f_body[1] = Configuration::getInstance()->getValue("Body1").asInt();//팔
	 f_body[2] = Configuration::getInstance()->getValue("Body2").asInt();//다리

	 return true;
 }

 void Blood::onKeyPressed(EventKeyboard::KeyCode keyCode, Event * event)
 {
	 switch (keyCode)
	 {
		case EventKeyboard::KeyCode::KEY_G:
		 this->getScene()->getPhysicsWorld()->setDebugDrawMask(
			 this->getScene()->getPhysicsWorld()->getDebugDrawMask() == PhysicsWorld::DEBUGDRAW_ALL ?
			 PhysicsWorld::DEBUGDRAW_NONE : PhysicsWorld::DEBUGDRAW_ALL);
		 break;
	 case EventKeyboard::KeyCode::KEY_F1:
	 {
		 d->setUserAngle(random(45, 90));
	 }
	 break;
	 case EventKeyboard::KeyCode::KEY_F2:
	 {
		 d->setUserRotation(random(0, 360));
	 }
	 break;
	 case EventKeyboard::KeyCode::KEY_SPACE:
		 if (Check == false)
		 {
			 Check_Number = d->isMatch(CheckBar->getRotation());
			 if (Check_Number > 0)
			 {
				 if (Check_Number == 2)
				 {
				     success = AudioEngine::play2d("mogi/sound/띠링.mp3");
				     CCLOG("영역 %d번", Check_Number);
				     blood_fill_temp_2 = blood + 0.125;
				     angry_fill_temp_2 = angry + 0.2;
				 }
				 if (Check_Number == 1)
				 {
					 auto CheckEFF = ParticleSystemQuad::create("mogi/Effect/CheckEff.plist");
					 CheckEFF->setPosition(Vec2(CheckBar->getPosition().x, CheckBar->getPosition().y));
					 this->addChild(CheckEFF);
					 blood_fill_temp = blood + 0.25;
					 success_2 = AudioEngine::play2d("mogi/sound/띠리링.mp3");
				 }
			 }
			 else
			 {
				 fail = AudioEngine::play2d("mogi/sound/에엥.mp3");
				 CCLOG("미스");
				 angry_fill_temp = angry + 0.5;
			 }
			 if (Check_Number > 0)
			 {
				CheckBar->runAction(Sequence::create(
					CallFunc::create([=]() {
							Check = true;
							d->setUserAngle(random(45, 90));
							d->setUserRotation(random(0, 360));
						}),
					DelayTime::create(2),
					CallFunc::create([=]() { Check = false; }), nullptr));
			 }
		 }
		 break;

	 case EventKeyboard::KeyCode::KEY_P:
		 Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiM::createScene(), Color3B(255, 255, 255)));
		 break;
	 }
 }
 void Blood::onKeyReleased(EventKeyboard::KeyCode keyCode, Event * event)
 {
	 //switch (keyCode)
	 //{
	 //}
 }

 void Blood::onMouseMove(cocos2d::EventMouse * event)
 {

 }

 void Blood::update(float delta)
 {
	 if (Check)
	 {
		 if (speed > 0)
		 {
			 speed -= delta * 370;
		 }
		 if (speed < 0)
		 {
			 speed = 0;
		 }
		 CheckBar->setRotation(speed);
	 }
	 if (Check == false)
	 {
		 speed += delta * 360 ;
		 if (speed >= 360) speed = ((int)speed % 360) + (speed - (int)speed);
		 CheckBar->setRotation(speed);
	 }

	 if (blood_fill_temp_2 > blood && blood < 1 && Check_Number == 2) blood += 0.0125;
	 if (angry_fill_temp_2 > angry && angry < 1 && Check_Number == 2) angry += 0.02;

	 if (blood_fill_temp > blood && blood < 1 && Check_Number == 1) blood += 0.025;
	 if (angry_fill_temp > angry && angry < 1 && Check_Number <= 0) angry += 0.05;


	 //if (angry < 1) angry += 0.5;

	 //if (blood < 1 && Check_Number == 1) blood += 0.25;



	 
	 if (blood >= 1 && replace == false)
	 {
		 if (f_body[0] == 1 && blood >= 1)
		 {
			 Configuration::getInstance()->setValue("Body0", Value(2));
		 }
		 if (f_body[1] == 1 && blood >= 1)
		 {
			 Configuration::getInstance()->setValue("Body1", Value(2));
		 }
		 if (f_body[2] == 1 && blood >= 1)
		 {
			 Configuration::getInstance()->setValue("Body2", Value(2));
		 }
		 replace = true;
		 temp_2 = 1 - blood;
		 blood += temp_2;
		 Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiM::createScene(), Color3B(255, 255, 255)));
	 }

	 if (angry >= 1 && mogi_b && replace == false)
	 {
		 if (f_body[0] == 1)
		 {
			 Configuration::getInstance()->setValue("Body0", Value(0));
		 }
		 if (f_body[1] == 1)
		 {
			 Configuration::getInstance()->setValue("Body1", Value(0));
		 }
		 if (f_body[2] == 1)
		 {
			 Configuration::getInstance()->setValue("Body2", Value(0));
		 }
		 replace = true;
		 temp_1 = 1 - angry;
		 angry += temp_1;
		 mogi_b = false;
		 runAction(Sequence::create(
			 DelayTime::create(1.0f), 
			 CallFunc::create([=]() {Director::getInstance()->replaceScene(TransitionTurnOffTiles::create(2.0f, MogiMove::createScene()));; }), 
			 nullptr));
		 
	 }
	 bloodBar->setScaleX(blood);
	 angryBar->setScaleX(angry);
 }
