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

Scene* MogiMove::createScene()
{
	auto scene	=	Scene::createWithPhysics(); // 물리에서 중요 //

	auto layer	= MogiMove::create();

	scene->addChild(layer);
	//물리적 중력 설정및 디버그 Draw
	scene->getPhysicsWorld()->setGravity(Vec2(0, -1000.0f));//물리적 중력
	scene->getPhysicsWorld()->setDebugDrawMask(PhysicsWorld::DEBUGDRAW_ALL);//디버그 Draw

	return scene;
}

bool MogiMove::init()
{
	if (!LayerColor::initWithColor(Color4B::BLACK))
	{
		return false;
	}
	speed = 350;
	pt4_angle = 0;

	this->scheduleUpdate();

	this->setKeyboardEnabled(true);
	////////////////////////////마우스 이벤트 /////////////////////////

	/*EventListenerMouse* mouse = EventListenerMouse::create();
	mouse->onMouseMove = CC_CALLBACK_1(MogiMove::onMouseMove, this);
	_eventDispatcher->addEventListenerWithSceneGraphPriority(mouse, this);*/

	///////////////////////////////충돌 이벤트 /////////////////////////////
	auto contactListener = EventListenerPhysicsContact::create();
	contactListener->onContactBegin = CC_CALLBACK_1(MogiMove::onContactBegin, this);
	_eventDispatcher->addEventListenerWithSceneGraphPriority(contactListener, this);

	AudioEngine::stopAll();

	MogiMove_bg = AudioEngine::play2d("mogi/sound/BOSS.mp3", true);
	AudioEngine::setVolume(MogiMove_bg, Mogimove_Sound_Scale);

	bg1 = Sprite::create("mogi/BG.png");
	bg1->setAnchorPoint(Vec2(0, 0));
	bg1->setPosition(Vec2(0, -12));
	this->addChild(bg1, 0);

	bg2 = Sprite::create("mogi/BG.png");
	bg2->setAnchorPoint(Vec2(0, 0));
	bg2->setPosition(Vec2(2059, -12));
	this->addChild(bg2, 0);

	bg1->runAction(RepeatForever::create(
		Sequence::create(
			MoveTo::create(5.0f, Vec2(-2059, -12)),
			MoveTo::create(0, Vec2(2059, -12)),
			MoveTo::create(5.0f, Vec2(0, -12)),
			nullptr)));
	bg2->runAction(RepeatForever::create(
		Sequence::create(
			MoveTo::create(5.0f, Vec2(0, -12)),
			MoveTo::create(5.0f, Vec2(-2059, -12)),
			MoveTo::create(0, Vec2(2059, -12)),
			nullptr)));

	AudioEngine::preload("mogi/sound/Hit1.mp3");
	AudioEngine::preload("mogi/sound/Hit2.mp3");
	AudioEngine::preload("mogi/sound/Hit3.mp3");
	AudioEngine::preload("mogi/sound/대쉬.mp3");


	//모기 

	mogi_Ani = Animation::create();
	mogi_Ani->setDelayPerUnit(0.05f);
	mogi_Ani->addSpriteFrameWithFile("mogi/body_1.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_2.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_3.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_4.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_4.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_5.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_6.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_7.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_8.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_9.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_10.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_11.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_12.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_13.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_14.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_15.png");
	mogi_Ani->addSpriteFrameWithFile("mogi/body_16.png");
	mogi_mate = Animate::create(mogi_Ani);

	SpritePlayer = Sprite::create();
	body = Sprite::create();
	Body_c = Sprite::create();

	this->addChild(body);
	this->addChild(SpritePlayer);
	body->addChild(Body_c);
	SpritePlayer->setVisible(false);

	body->setScale(0.8f);
	SpritePlayer->runAction(RepeatForever::create(mogi_mate));

	body->setPosition(Vec2(1280, 720) / 2);
	Body_c->runAction(RepeatForever::create(mogi_mate->clone()));
	body->setFlipX(true);
	Body_c->setFlipX(true);

	physics = PhysicsBody::createCircle((45.0f), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(10, 0));
	physics->setDynamic(false);
	physics->setCategoryBitmask((int)PhysicsCategory::Hero);
	physics->setCollisionBitmask((int)PhysicsCategory::None);
	physics->setContactTestBitmask((int)PhysicsCategory::hand);
	physics->setRotationEnable(false);
	body->setPhysicsBody(physics);
	physics->setGravityEnable(false);

	/*hand_ani = Animation::create();
	hand_ani->setDelayPerUnit(0.25f);
	hand_ani->addSpriteFrameWithFile("mogi/hd1.png");
	hand_ani->addSpriteFrameWithFile("mogi/hd2.png");
	hand_ani->addSpriteFrameWithFile("mogi/hd3.png");
	hand_ani->addSpriteFrameWithFile("mogi/hd4.png");
	
	hand_mate = Animate::create(hand_ani);

	SpriteBoss = Sprite::create();
	Boss = Sprite::create();
	Boss_c = Sprite::create();

	Boss->setAnchorPoint(Vec2::ZERO);
	Boss->setPosition(Vec2(640,360));

	Boss_c->setRotation(-8.0f);

	this->addChild(Boss);
	this->addChild(SpriteBoss);
	Boss->addChild(Boss_c);
	SpriteBoss->setVisible(false);
	SpriteBoss->runAction(RepeatForever::create(hand_mate));
	Boss_c->runAction(RepeatForever::create(hand_mate->clone()));

	H_phy = PhysicsBody::createBox(Size(200,200), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(10, 0));
	H_phy->setDynamic(false);
	H_phy->setCategoryBitmask((int)PhysicsCategory::hand);
	H_phy->setCollisionBitmask((int)PhysicsCategory::None);
	H_phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	H_phy->setRotationEnable(false);
	Boss->setPhysicsBody(H_phy);
	H_phy->setGravityEnable(false);*/
	//패턴 1.//
	Pt1_1 = Sprite::create("mogi/패턴1.png");
	Pt1_1->setAnchorPoint(Vec2::ZERO);
	Pt1_1->setPosition(Vec2(0, 1300));
	Pt1_1->setContentSize(Size(500, 790));

	this->addChild(Pt1_1);

	PT1_phy = PhysicsBody::createBox(Pt1_1->getContentSize(), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
	PT1_phy->setDynamic(false);
	PT1_phy->setCategoryBitmask((int)PhysicsCategory::hand);
	PT1_phy->setCollisionBitmask((int)PhysicsCategory::None);
	PT1_phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	PT1_phy->setRotationEnable(false);
	Pt1_1->setPhysicsBody(PT1_phy);
	PT1_phy->setGravityEnable(false);

	Pt1_2 = Sprite::create("mogi/패턴1.png");
	Pt1_2->setAnchorPoint(Vec2::ZERO);
	Pt1_2->setPosition(Vec2(780, 1300));
	Pt1_2->setContentSize(Size(500,790));

	this->addChild(Pt1_2);

	PT1_1_phy = PhysicsBody::createBox(Pt1_2->getContentSize(), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
	PT1_1_phy->setDynamic(false);
	PT1_1_phy->setCategoryBitmask((int)PhysicsCategory::hand);
	PT1_1_phy->setCollisionBitmask((int)PhysicsCategory::None);
	PT1_1_phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	PT1_1_phy->setRotationEnable(false);
	Pt1_2->setPhysicsBody(PT1_1_phy);
	PT1_1_phy->setGravityEnable(false);
													
	Pt1_danger_1 = Sprite::create("mogi/danger.png");
	Pt1_danger_2 = Sprite::create("mogi/danger.png");

	Pt1_danger_1->setContentSize(Size(500,720));
	Pt1_danger_2->setContentSize(Size(500, 720));

	Pt1_danger_1->setAnchorPoint(Vec2::ZERO);
	Pt1_danger_2->setAnchorPoint(Vec2::ZERO);

	Pt1_danger_1->setPosition(Vec2::ZERO);
	Pt1_danger_2->setPosition(Vec2(780,0));

	Pt1_danger_1->setVisible(false);
	Pt1_danger_2->setVisible(false);

	Pt1_danger_1->setOpacity(120);
	Pt1_danger_2->setOpacity(120);

	this->addChild(Pt1_danger_1);
	this->addChild(Pt1_danger_2);
	//패턴 2.//
	Pt2_1 = Sprite::create("mogi/패턴2.png");
	Pt2_1->setAnchorPoint(Vec2::ZERO);
	Pt2_1->setPosition(Vec2(1300, 720));
	Pt2_1->setContentSize(Size(360, 790));
	Pt2_1->setRotation(90.0f);

	this->addChild(Pt2_1);

	PT2_phy = PhysicsBody::createBox(Size(790,360), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
	PT2_phy->setDynamic(false);
	PT2_phy->setCategoryBitmask((int)PhysicsCategory::hand);
	PT2_phy->setCollisionBitmask((int)PhysicsCategory::None);
	PT2_phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	PT2_phy->setRotationEnable(false);
	Pt2_1->setPhysicsBody(PT2_phy);
	PT2_phy->setGravityEnable(false);

	Pt2_2 = Sprite::create("mogi/패턴2.png");
	Pt2_2->setAnchorPoint(Vec2::ZERO);
	Pt2_2->setPosition(Vec2(-810, 720));
	Pt2_2->setContentSize(Size(360, 790));
	Pt2_2->setRotation(90.0f);
	Pt2_2->setFlippedY(true);

	this->addChild(Pt2_2);

	PT2_1_phy = PhysicsBody::createBox(Size(790, 360), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
	PT2_1_phy->setDynamic(false);
	PT2_1_phy->setCategoryBitmask((int)PhysicsCategory::hand);
	PT2_1_phy->setCollisionBitmask((int)PhysicsCategory::None);
	PT2_1_phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	PT2_1_phy->setRotationEnable(false);
	Pt2_2->setPhysicsBody(PT2_1_phy);
	PT2_1_phy->setGravityEnable(false);

	Pt2_danger_1 = Sprite::create("mogi/danger.png");
	Pt2_danger_2 = Sprite::create("mogi/danger.png");
	  
	Pt2_danger_1->setContentSize(Size(1280, 360));
	Pt2_danger_2->setContentSize(Size(1280, 360));
	  
	Pt2_danger_1->setAnchorPoint(Vec2::ZERO);
	Pt2_danger_2->setAnchorPoint(Vec2::ZERO);
	  
	Pt2_danger_1->setPosition(Vec2(0,360));
	Pt2_danger_2->setPosition(Vec2::ZERO);
	  
	Pt2_danger_1->setVisible(false);
	Pt2_danger_2->setVisible(false);
	  
	Pt2_danger_1->setOpacity(120);
	Pt2_danger_2->setOpacity(120);

	this->addChild(Pt2_danger_1);
	this->addChild(Pt2_danger_2);
	//패턴 3.//
	Pt3_1 = Sprite::create("mogi/패턴2.png");
	Pt3_1->setAnchorPoint(Vec2(0, 0));
	Pt3_1->setContentSize(Size(360, 790));
	Pt3_1->setPosition(Vec2(1300, 720 -180));
	Pt3_1->setRotation(90.0f);

	this->addChild(Pt3_1);

	PT3_phy = PhysicsBody::createBox(Size(790, 360), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
	PT3_phy->setDynamic(false);
	PT3_phy->setCategoryBitmask((int)PhysicsCategory::hand);
	PT3_phy->setCollisionBitmask((int)PhysicsCategory::None);
	PT3_phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	PT3_phy->setRotationEnable(false);
	Pt3_1->setPhysicsBody(PT3_phy);
	PT3_phy->setGravityEnable(false);

	Pt3_2 = Sprite::create("mogi/패턴2.png");
	Pt3_2->setAnchorPoint(Vec2(0,0));
	Pt3_2->setContentSize(Size(360, 790));
	Pt3_2->setPosition(Vec2(-810, 720 - 180));
	Pt3_2->setRotation(90.0f);
	Pt3_2->setFlippedY(true);

	this->addChild(Pt3_2);

	PT3_1_phy = PhysicsBody::createBox(Size(790, 360), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
	PT3_1_phy->setDynamic(false);
	PT3_1_phy->setCategoryBitmask((int)PhysicsCategory::hand);
	PT3_1_phy->setCollisionBitmask((int)PhysicsCategory::None);
	PT3_1_phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	PT3_1_phy->setRotationEnable(false);
	Pt3_2->setPhysicsBody(PT3_1_phy);
	PT3_1_phy->setGravityEnable(false);
	  
	Pt3_danger_1 = Sprite::create("mogi/danger.png");
	Pt3_danger_2 = Sprite::create("mogi/danger.png");
	  
	Pt3_danger_1->setContentSize(Size(640, 540));
	Pt3_danger_2->setContentSize(Size(640, 540));
	  
	Pt3_danger_1->setAnchorPoint(Vec2(0, 0));
	Pt3_danger_2->setAnchorPoint(Vec2(0, 0));

	Pt3_danger_1->setPosition(Vec2(0, 0));
	Pt3_danger_2->setPosition(Vec2(640, 180));
	  
	Pt3_danger_1->setVisible(false);
	Pt3_danger_2->setVisible(false);
	  
	Pt3_danger_1->setOpacity(120);
	Pt3_danger_2->setOpacity(120);

	this->addChild(Pt3_danger_1);
	this->addChild(Pt3_danger_2); 
	//패턴4.//
	Pt4_1 = Sprite::create("mogi/패턴4_1.png");
	Pt4_1->setAnchorPoint(Vec2(2, 1));
	Pt4_1->setContentSize(Size(360, 200));
	Pt4_1->setPosition(Vec2(675, 0));

	this->addChild(Pt4_1);

	PT4_phy = PhysicsBody::createBox(Size(360, 200), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
	PT4_phy->setDynamic(false);
	PT4_phy->setCategoryBitmask((int)PhysicsCategory::hand);
	PT4_phy->setCollisionBitmask((int)PhysicsCategory::None);
	PT4_phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	PT4_phy->setRotationEnable(true);
	Pt4_1->setPhysicsBody(PT4_phy);
	PT4_phy->setGravityEnable(false);

	  
	Pt4_2 = Sprite::create("mogi/패턴4_2.png");
	Pt4_2->setAnchorPoint(Vec2(-1, 1));
	Pt4_2->setContentSize(Size(360, 200));
	Pt4_2->setPosition(Vec2(1280 - 675, 0));

	this->addChild(Pt4_2);

	PT4_1_phy = PhysicsBody::createBox(Size(360, 200), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
	PT4_1_phy->setDynamic(false);
	PT4_1_phy->setCategoryBitmask((int)PhysicsCategory::hand);
	PT4_1_phy->setCollisionBitmask((int)PhysicsCategory::None);
	PT4_1_phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	PT4_1_phy->setRotationEnable(false);
	Pt4_2->setPhysicsBody(PT4_1_phy);
	PT4_1_phy->setGravityEnable(false);
	  
	Pt4_danger_1 = Sprite::create("mogi/danger.png");
	Pt4_danger_2 = Sprite::create("mogi/danger.png");
	  
	Pt4_danger_1->setContentSize(Size(360, 200));
	Pt4_danger_2->setContentSize(Size(360, 200));
	  
	Pt4_danger_1->setAnchorPoint(Vec2(0, 0));
	Pt4_danger_2->setAnchorPoint(Vec2(1, 0));
	  
	Pt4_danger_1->setPosition(Vec2(0, 0));
	Pt4_danger_2->setPosition(Vec2(1280, 0));
	  
	Pt4_danger_1->setVisible(false);
	Pt4_danger_2->setVisible(false);
	  
	Pt4_danger_1->setOpacity(120); 
	Pt4_danger_2->setOpacity(120);

	this->addChild(Pt4_danger_1);
	this->addChild(Pt4_danger_2);

	Gameover_bg = Sprite::create("mogi/게임오버.png");
	Gameover_bg->setAnchorPoint(Vec2::ZERO);
	Gameover_bg->setContentSize(Size(950, 500));
	Gameover_bg->setPosition(Vec2(165, 0));
	this->addChild(Gameover_bg);

	Gameover_bg->runAction(FadeOut::create(0));


	auto Gameover_main_2 = Sprite::create("mogi/메인메뉴.png");
	Gameover_main_2->setColor(Color3B(170, 170, 170));
	Gameover_main = MenuItemSprite::create(Sprite::create("mogi/메인메뉴.png"), Gameover_main_2, CC_CALLBACK_1(MogiMove::Main_menu, this));
	Gameover_main->setPosition(-400, -220);

	auto Gameover_restart_2 = Sprite::create("mogi/다시하기.png");
	Gameover_restart_2->setColor(Color3B(170, 170, 170));
	Gameover_restart = MenuItemSprite::create(Sprite::create("mogi/다시하기.png"), Gameover_restart_2, CC_CALLBACK_1(MogiMove::Main_restart, this));
	Gameover_restart->setPosition(80, -220);

	auto Gameover_menu = Menu::create(Gameover_main, Gameover_restart, NULL);
	Gameover_menu->setAnchorPoint(Vec2::ZERO);
	Gameover_main->runAction(FadeOut::create(0));
	Gameover_restart->runAction(FadeOut::create(0));
	Gameover_bg->addChild(Gameover_menu);
	
	Time = Label::createWithSystemFont("", "Marker Felt.ttf", 50);
	Time->setPosition(Vec2(640, 650));
	this->addChild(Time);
}

void MogiMove::Main_menu(Ref* pSender)
{
	if (Gameover == true)
	{
		Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiMain::createScene(), Color3B(255, 255, 255)));
	}
}

void MogiMove::Main_restart(Ref * pSender)
{
	if (Gameover == true)
	{
		Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiM::createScene(), Color3B(255, 255, 255)));
	}
}
	
 void MogiMove::onKeyPressed(EventKeyboard::KeyCode keyCode, Event * event)
 {
	 switch (keyCode)
	 {
	 case EventKeyboard::KeyCode::KEY_UP_ARROW:
		 up = true;
		 down = false;
		 break;

	 case EventKeyboard::KeyCode::KEY_LEFT_ARROW:
		 left = true;
		 right = false;
		 break;
	 case EventKeyboard::KeyCode::KEY_DOWN_ARROW:
		 up = false;
		 down = true;
		 break;
	 case EventKeyboard::KeyCode::KEY_RIGHT_ARROW:
		 right = true;
		 left = false;
		 break;
	 case EventKeyboard::KeyCode::KEY_SHIFT:
		 if (dash == false && dashCool <= 0)
		 {
			 dash = true;
			 dashCool = 0;
			 body->runAction(CallFunc::create([=]() {
				 auto Dash_ef = ParticleSystemQuad::create("mogi/Effect/Dash.plist");
				 Dash_ef->setPosition(Vec2(0, 0));
				 body->addChild(Dash_ef);

				 Dash_sound = AudioEngine::play2d("mogi/sound/대쉬.mp3");
				 ; }));
		 }
		 break;
	 case EventKeyboard::KeyCode::KEY_ESCAPE:

		 if (Ps == true)
		 {
			 Director::sharedDirector()->resume();
			 Ps = false;
		 }
		 else if (Ps == false)
		 {
			 Director::sharedDirector()->pause();
			 Ps = true;
		 }
		 break;
	 case EventKeyboard::KeyCode::KEY_1:

		 if (use_pt1 == false)
		 {
			 runAction(Sequence::create(
				 CallFunc::create([=]() {Pt1(); }),
				DelayTime::create(3),
				 CallFunc::create([=]() {Pt3(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt1(); }),
				 nullptr));
		 }
		 use_pt1 = true;
		 break;
	 case EventKeyboard::KeyCode::KEY_2:

		 if (use_pt2 == false)
		 {
			 runAction(Sequence::create(
				 CallFunc::create([=]() {Pt3(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt2(); }),
				 DelayTime::create(6),
				 CallFunc::create([=]() {Pt1(); }),
				 nullptr));
		 }
		 use_pt2 = true;
		 break;
	 case EventKeyboard::KeyCode::KEY_3:
		 if (use_pt3 == false)
		 {
			 runAction(Sequence::create(
				 CallFunc::create([=]() {Pt2(); }),
				 DelayTime::create(6),
				 CallFunc::create([=]() {Pt1(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt3(); }),
				 nullptr));
		 }
		 use_pt3 = true;
		 break;
	 case EventKeyboard::KeyCode::KEY_4:

		 if (use_pt4 == false)
		 {
			 runAction(Sequence::create(
				 CallFunc::create([=]() {Pt4(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt1(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt3(); }),
				 nullptr));
		 }
		 use_pt4 = true;
		 break;
	 case EventKeyboard::KeyCode::KEY_5:

		 if (use_pt5 == false)
		 {
			 runAction(Sequence::create(
				 CallFunc::create([=]() {Pt3(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt4(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt2(); }),
				 nullptr));
		 }
		 use_pt5 = true;
		 break;
	 case EventKeyboard::KeyCode::KEY_SPACE:
		 break;

	 case EventKeyboard::KeyCode::KEY_G:
		 this->getScene()->getPhysicsWorld()->setDebugDrawMask(
			 this->getScene()->getPhysicsWorld()->getDebugDrawMask() == PhysicsWorld::DEBUGDRAW_ALL ?
			 PhysicsWorld::DEBUGDRAW_NONE : PhysicsWorld::DEBUGDRAW_ALL);
		 break;

	 case EventKeyboard::KeyCode::KEY_P:
		 Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiM::createScene(), Color3B(255, 255, 255)));
		 break;
	 case EventKeyboard::KeyCode::KEY_L:
		 Pattern_rand_b = true;
		 Pattern_random = random(1, 4);
		 
		 break;
	 }
 }
 void MogiMove::onKeyReleased(EventKeyboard::KeyCode keyCode, Event * event)
 {
	 switch (keyCode)
	 {
	 case EventKeyboard::KeyCode::KEY_UP_ARROW:
		 up = false;
		 pos = body->getPhysicsBody()->getVelocity();
		 body->getPhysicsBody()->setVelocity(Vec2(pos.x, 0));
		 break;
	 case EventKeyboard::KeyCode::KEY_LEFT_ARROW:
		 left = false;
		 pos = body->getPhysicsBody()->getVelocity();
		 body->getPhysicsBody()->setVelocity(Vec2(0, pos.y));
		 break;
	 case EventKeyboard::KeyCode::KEY_DOWN_ARROW:
		 down = false;
		 pos = body->getPhysicsBody()->getVelocity();
		 body->getPhysicsBody()->setVelocity(Vec2(pos.x, 0));
		 break;
	 case EventKeyboard::KeyCode::KEY_RIGHT_ARROW:
		 right = false;
		 pos = body->getPhysicsBody()->getVelocity();
		 body->getPhysicsBody()->setVelocity(Vec2(0, pos.y));
		 break;
	 }
 }
 bool MogiMove::onContactBegin(PhysicsContact & contact)
 {
	 Node* nodeHero = nullptr;
	 Node* nodeHand = nullptr;

	 PhysicsShape* shape[2] = { contact.getShapeA(), contact.getShapeB() };

	 for (int i = 0; i < 2; i++)
	 {
		 switch (shape[i]->getCategoryBitmask())
		 {
		 case (int)PhysicsCategory::Hero:
		 {
			 nodeHero = shape[i]->getBody()->getNode();
			 break;
		 }
		 case (int)PhysicsCategory::hand:
		 {
			 nodeHand = shape[i]->getBody()->getNode();
			 break;
		 }
		 }
	 }
	 if (nodeHero != nullptr)
	 {
		 if (nodeHand != nullptr && Gameover == false)
		 {
			 Gameover = true;

			 auto Blood_eff = ParticleSystemQuad::create("mogi/Effect/Blood.plist");
			 Blood_eff->setPosition(Vec2(body->getPosition().x, body->getPosition().y));
			 this->addChild(Blood_eff);

			 Hit_sound = random(1, 3);
			 switch (Hit_sound)
			 {
			 case 1:
				 Hit_sound_1 = AudioEngine::play2d("mogi/sound/Hit1.mp3");
				 break;
			 case 2:
				 Hit_sound_2 = AudioEngine::play2d("mogi/sound/Hit2.mp3");

				 break;
			 case 3:
				 Hit_sound_3 = AudioEngine::play2d("mogi/sound/Hit3.mp3");

				 break;
			 default:
				 break;
			 }
			 body->getPhysicsBody()->setVelocity(Vec2(0, -450));
			 Gameover_bg->runAction(
				 Spawn::create(
					 MoveBy::create(1, Vec2(0, 110)),
					 FadeIn::create(1), nullptr));
			 Gameover_main->runAction(FadeIn::create(1));
			 Gameover_restart->runAction(FadeIn::create(1));
		 }
	 }
	 return true;
 }
 void MogiMove::update(float delta)
 {

	 if (Mogimove_Sound_Scale < 0.5)
	 {
		 Mogimove_Sound_Scale += 0.0025;
	 }
	 AudioEngine::setVolume(MogiMove_bg, Mogimove_Sound_Scale);


	 if (end_time > 0)
	 {
		 end_time -= delta;
	 	 Time->setString(StringUtils::format("남은 시간 : %.0f", end_time));
	 }

	 if (end_time <= 0)
	 {
		 end_time = 1000;
		 Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiM::createScene(), Color3B(255, 255, 255)));
	 }
	 if (up) {
		 pos = body->getPhysicsBody()->getVelocity();
		 body->getPhysicsBody()->setVelocity(Vec2(pos.x, speed));
	 }
	 if (down) {
		 pos = body->getPhysicsBody()->getVelocity();
		 body->getPhysicsBody()->setVelocity(Vec2(pos.x, -speed));
	 }
	 if (left) {
		 pos = body->getPhysicsBody()->getVelocity();
		 body->getPhysicsBody()->setVelocity(Vec2(-speed, pos.y));

		 body->setFlipX(false);
		 Body_c->setFlipX(false);
	 }
	 if (right) {
		 pos = body->getPhysicsBody()->getVelocity();
		 body->getPhysicsBody()->setVelocity(Vec2(speed, pos.y));

		 body->setFlipX(true);
		 Body_c->setFlipX(true);
	 }
	 if (dashCool > 0)
	 {
		 dashCool -= delta;
	 }

	 if (speed > 350)
	 {
		 god = true;
		 speed *= 0.75;
	 }
	 if (speed <= 350)
	 {
		 speed = 350;
		 god = false;
	 }

	 if (dashCool <= 0 && dash)
	 {
		 dashCool = 3;
		 speed = 5000;
		 dash = false;
	 }

	 if (body->getPosition().x - 32 < 0)
	 {
		 body->setPosition(Vec2(32, body->getPosition().y));
	 }
	 if (body->getPosition().x + 32 > 1280)
	 {
		 body->setPosition(Vec2(1280 - 32, body->getPosition().y));
	 }
	 if (body->getPosition().y - 32 < 0 && Gameover == false)
	 {
		 body->setPosition(Vec2(body->getPosition().x, 32));
	 }
	 if (body->getPosition().y + 32 > 720)
	 {
		 body->setPosition(Vec2(body->getPosition().x, 720 - 32));
	 }

	 if (Pt1_1->getPositionY() <= 0 || Pt1_2->getPositionY() <= 0)
	 {
		 shake = Shake::create(0.08f, 8, 8);
		 this->runAction(shake);
	 }

	 if (Gameover)
	 {
		 Gameover_rotate += 18;
		 down = true;
	 }

	 if (Pattern_rand_b)
	 {
		 switch (Pattern_random)
		 {
		 case 1:
			 runAction(Sequence::create(
				 CallFunc::create([=]() {Pattern_rand_b = false; }),
				 CallFunc::create([=]() {Pt1(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt3(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt1(); }),
				 DelayTime::create(5),
				 CallFunc::create([=]() {Pattern_random = random(1, 5); Pattern_rand_b = true; }),
				 nullptr));
			 break;
		 case 2:
			 runAction(Sequence::create(
				 CallFunc::create([=]() { Pattern_rand_b = false; }),
				 CallFunc::create([=]() {Pt3(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt2(); }),
				 DelayTime::create(5),
				 CallFunc::create([=]() {Pt1(); }),
				 DelayTime::create(5),
				 CallFunc::create([=]() {Pattern_random = random(1, 5); Pattern_rand_b = true;   }),
				 nullptr));
			 break;
		 case 3:
			 runAction(Sequence::create(
				 CallFunc::create([=]() {Pattern_rand_b = false; }),
				 CallFunc::create([=]() {Pt2(); }),
				 DelayTime::create(6),
				 CallFunc::create([=]() {Pt1(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt3(); }),
				 DelayTime::create(5),
				 CallFunc::create([=]() {Pattern_random = random(1, 5); Pattern_rand_b = true; }),
				 nullptr));
			 break;
		 case 4:
			 runAction(Sequence::create(
				 CallFunc::create([=]() {Pattern_rand_b = false; }),
				 CallFunc::create([=]() {Pt4(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt1(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt3(); }),
				 DelayTime::create(5),
				 CallFunc::create([=]() {Pattern_random = random(1, 5); Pattern_rand_b = true; }),
				 nullptr));

		 case 5:
			 runAction(Sequence::create(
				 CallFunc::create([=]() {Pattern_rand_b = false; }),
				 CallFunc::create([=]() {Pt3(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt4(); }),
				 DelayTime::create(3),
				 CallFunc::create([=]() {Pt2(); }),
				 DelayTime::create(6),
				 CallFunc::create([=]() {Pattern_random = random(1, 5); Pattern_rand_b = true; }),
				 nullptr));
			 break;
		 default:
			 break;
		 }
	}
	 body->setRotation(Gameover_rotate);
 }

 void MogiMove::Pt1()
 {
	 Pt1_1->runAction(
		 Sequence::create(
			 CallFunc::create([=]() {
				 Pt1_danger_1->setVisible(true);
				 Pt1_danger_2->setVisible(true);
				 }),
			 DelayTime::create(1.0f)
			 ,
			 CallFunc::create([=]() {
			 Pt1_danger_1->setVisible(false);
			 Pt1_danger_2->setVisible(false);
			 }),
			 CallFunc::create([=]() {
			 Pt1_1->getPhysicsBody()->setVelocity(Vec2(0,-2500)); 
			 }),
			 DelayTime::create(1.2f)
			 ,
			 Place::create(Vec2(0,1300)), 
			 CallFunc::create([=]() {
				 Pt1_1->getPhysicsBody()->setVelocity(Vec2(0, 0));
				 }),nullptr));
	 Pt1_2->runAction(
		 Sequence::create(
			 DelayTime::create(1.5f),
			 CallFunc::create([=]() {
				 Pt1_2->getPhysicsBody()->setVelocity(Vec2(0, -2500));
				 }),
			 DelayTime::create(1.2f) ,
					 Place::create(Vec2(780, 1300)),
					 CallFunc::create([=]() {
					 Pt1_2->getPhysicsBody()->setVelocity(Vec2(0, 0));
					 use_pt1 = false;
						 }), nullptr));
 }

 void MogiMove::Pt2()
 {
	 Pt2_1->runAction(
		 Sequence::create(
		 CallFunc::create([=]() {
			 Pt2_danger_1->setVisible(true);
			 }),
		 DelayTime::create(1.0f),
		 CallFunc::create([=]() {
			 Pt2_danger_1->setVisible(false);
			 }),
		 CallFunc::create([=]() {
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(-600, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(600, 0));
			 }),
			 DelayTime::create(0.4f),
			 CallFunc::create([=]() {
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(-2600, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(2600, 0));
				 }),
			 DelayTime::create(0.1725f),
			 CallFunc::create([=]() {
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 shake = Shake::create(0.1f, 15, 8);
			 this->runAction(shake);
				 }),
			 DelayTime::create(0.28f),
			 CallFunc::create([=]() {
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(1500, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(-1500, 0));
				 }),
			 DelayTime::create(0.8f), 
			 CallFunc::create([=]() {
			 Pt2_1->runAction(Place::create(Vec2(1300, 360)));
			 Pt2_2->runAction(Place::create(Vec2(-810, 360)));
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 }),
			 DelayTime::create(0.2f),
			 CallFunc::create([=]() {
			 Pt2_danger_2->setVisible(true);
					 }),
			 DelayTime::create(1.0f)
			 ,
			 CallFunc::create([=]() {
			 Pt2_danger_2->setVisible(false);
				 }),
				 CallFunc::create([=]() {
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(-600, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(600, 0));
					 }),
			 DelayTime::create(0.4f),
			 CallFunc::create([=]() {
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(-2600, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(2600, 0));
				 }),
			 DelayTime::create(0.1725f),
			 CallFunc::create([=]() {
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 shake = Shake::create(0.1f, 15, 8);
			 this->runAction(shake);
				 }),
			 DelayTime::create(0.28f),
			 CallFunc::create([=]() {
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(1500, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(-1500, 0));
				 }),
			 DelayTime::create(1.0f),
			 CallFunc::create([=]() {
			 Pt2_1->runAction(Place::create(Vec2(1300, 720)));
			 Pt2_2->runAction(Place::create(Vec2(-810, 720)));
			 Pt2_1->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 Pt2_2->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 use_pt2 = false;
				 }),
			 nullptr));
 }

 void MogiMove::Pt3()
 {
	 Pt3_1->runAction(
		 Sequence::create(
			 CallFunc::create([=]() {
				 Pt3_danger_1->setVisible(true);
				 Pt3_danger_2->setVisible(true);
				 }),
			 DelayTime::create(1.0f),
			 CallFunc::create([=]() {
			 Pt3_danger_1->setVisible(false); 
			 Pt3_danger_2->setVisible(false);
				 }),
			 CallFunc::create([=]() {
			 Pt3_1->getPhysicsBody()->setVelocity(Vec2(-600, 0));
			 Pt3_2->getPhysicsBody()->setVelocity(Vec2(600, 0));
				 }),
			 DelayTime::create(0.4f),
			 CallFunc::create([=]() {
			 Pt3_1->getPhysicsBody()->setVelocity(Vec2(-2600, 0));
			 Pt3_2->getPhysicsBody()->setVelocity(Vec2(2600, 0));
				 }),
			 DelayTime::create(0.1725f),
			 CallFunc::create([=]() {
			 Pt3_1->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 Pt3_2->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 shake = Shake::create(0.1f, 15, 8);
			 this->runAction(shake);
				 }),
			 DelayTime::create(0.28f),
			 CallFunc::create([=]() {
			 Pt3_1->getPhysicsBody()->setVelocity(Vec2(0, 1500));
			 Pt3_2->getPhysicsBody()->setVelocity(Vec2(0, -1500));
				 }),
			 DelayTime::create(1.0f), 
			 CallFunc::create([=]() {
			 Pt3_1->runAction(Place::create(Vec2(1300, 540)));
			 Pt3_2->runAction(Place::create(Vec2(-810, 540)));
			 Pt3_1->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 Pt3_2->getPhysicsBody()->setVelocity(Vec2(0, 0));
			 use_pt3 = false;
				 }),
			  nullptr));
 }

 void MogiMove::Pt4()
 {
	 Pt4_1->runAction(
		 Sequence::create(
			 Spawn::create(RotateBy::create(1.0f, 90),
				 CallFunc::create([=] {Pt4_2->runAction(RotateBy::create(1.0f, -90)); }), nullptr),
			 CallFunc::create([=] {shake = Shake::create(0.1f, 15, 8); this->runAction(shake); }),
			 DelayTime::create(0.5f),
			 Spawn::create(RotateBy::create(1.0f, -90), 
				 CallFunc::create([=] {Pt4_2->runAction(RotateBy::create(1.0f, 90));
					 }), nullptr),
			 CallFunc::create([=] {	 
						 use_pt4 = false;
						 }), nullptr));
 }

