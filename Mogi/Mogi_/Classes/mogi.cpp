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

Scene* Mogi::createScene()

{
	auto scene	=	Scene::createWithPhysics(); // 물리에서 중요 //

	auto layer	= Mogi::create();

	scene->addChild(layer);
	//물리적 중력 설정및 디버그 Draw
	scene->getPhysicsWorld()->setGravity(Vec2(0, -1000.0f));//물리적 중력
	//scene->getPhysicsWorld()->setDebugDrawMask(PhysicsWorld::DEBUGDRAW_ALL);//디버그 Draw

	return scene;
}

bool Mogi::init()
{
	if (!LayerColor::initWithColor(Color4B::BLACK))
	{
		return false;
	}

	Efv = 0;
	PsT = 0;
	Pco = 1;

	AudioEngine::stopAll(); 

	this->scheduleUpdate();

	this->setKeyboardEnabled(true);
	////////////////////////////마우스 이벤트 /////////////////////////

	EventListenerMouse* mouse = EventListenerMouse::create();
	mouse->onMouseMove = CC_CALLBACK_1(Mogi::onMouseMove, this); 
	_eventDispatcher->addEventListenerWithSceneGraphPriority(mouse, this);

	///////////////////////////////충돌 이벤트 /////////////////////////////
	auto contactListener = EventListenerPhysicsContact::create();
	contactListener->onContactBegin = CC_CALLBACK_1(Mogi::onContactBegin, this);
	_eventDispatcher->addEventListenerWithSceneGraphPriority(contactListener, this);
	//////////////////////////////////////바탕//////////////////
	bg1 = Sprite::create("mogi/BG.png");
	bg1->setAnchorPoint(Vec2(0, 0));
	bg1->setPosition(Vec2(0, -12));
	this->addChild(bg1, 0);

	bg2 = Sprite::create("mogi/BG.png");
	bg2->setAnchorPoint(Vec2(0, 0));
	bg2->setPosition(Vec2(2059, -12));
	this->addChild(bg2, 0);

	mosquito = AudioEngine::play2d("mogi/sound/mosquito.mp3", true);//뒤에 true가 붙으면 무한재생
	AudioEngine::setVolume(mosquito, 0.025);

	AudioEngine::preload("mogi/sound/Hit1.mp3");
	AudioEngine::preload("mogi/sound/Hit2.mp3");
	AudioEngine::preload("mogi/sound/Hit3.mp3");

	//int bgm = AudioEngine::play2d("mogi/sound/게임진행.mp3", true);//뒤에 true가 붙으면 무한재생
	

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

	body->setPosition(Vec2(128, 360));
	Body_c->runAction(RepeatForever::create(mogi_mate->clone()));

	body->setFlipX(true);
	Body_c->setFlipX(true);

	physics = PhysicsBody::createCircle((45.0f), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(10,0));
	physics->setDynamic(false);
	physics->setCategoryBitmask((int)PhysicsCategory::Hero);
	physics->setCollisionBitmask((int)PhysicsCategory::None);
	physics->setContactTestBitmask((int)PhysicsCategory::Spider | (int)PhysicsCategory::Button | (int)PhysicsCategory::Eat);
	physics->setRotationEnable(false); 
	body->setPhysicsBody(physics);
	physics->setGravityEnable(false);

	for (int i = 0; i < 5; i++)
	{
		gumijul[i] = Sprite::create("mogi/gumijul.png");
		gumijul[i]->setContentSize(Size(340,720 / 5 ));
		gumijul[i]->setAnchorPoint(Vec2::ZERO);
		gumijul[i]->setPosition(Vec2(1500, 1000));

		G_Phy = PhysicsBody::createBox(Size(370, 240), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
		G_Phy->setDynamic(false);
		G_Phy->setCategoryBitmask((int)PhysicsCategory::Spider);
		G_Phy->setCollisionBitmask((int)PhysicsCategory::None);
		G_Phy->setContactTestBitmask((int)PhysicsCategory::Hero);
		G_Phy->setRotationEnable(false);
		gumijul[i]->setPhysicsBody(G_Phy);
		G_Phy->setGravityEnable(false);

		gumi[i] = Sprite::create("mogi/gumi.png");
		gumi[i]->setAnchorPoint(Vec2::ZERO);
		gumi[i]->setPosition(Vec2(20, 0));

		this->addChild(gumijul[i]);
		gumijul[i]->addChild(gumi[i]);

		G_Phy = PhysicsBody::createBox(Size(70,240),PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
		G_Phy->setDynamic(false);
		G_Phy->setCategoryBitmask((int)PhysicsCategory::Spider);
		G_Phy->setCollisionBitmask((int)PhysicsCategory::None);
		G_Phy->setContactTestBitmask((int)PhysicsCategory::Hero);
		G_Phy->setRotationEnable(false);
		gumi[i]->setPhysicsBody(G_Phy);
		G_Phy->setGravityEnable(false);
	}

	for (int i = 0; i < 5; i++)
	{
		fish_Ani = Animation::create();
		fish_Ani->setDelayPerUnit(0.15f);
		fish_Ani->addSpriteFrameWithFile("mogi/fish1.png");
		fish_Ani->addSpriteFrameWithFile("mogi/fish2.png");
		fish_Ani->addSpriteFrameWithFile("mogi/fish3.png");
		fish_mate = Animate::create(fish_Ani);

		s_fish = Sprite::create();
		fish[i] = Sprite::create();
		fish_c[i] = Sprite::create();

		fish_c[i]->setAnchorPoint(Vec2::ZERO);
		fish_c[i]->setScale(1.8f);
		fish[i]->setContentSize(Size(370, 720 / 3));

		fish[i]->setAnchorPoint(Vec2::ZERO);
		fish[i]->setPosition(Vec2(1500, 720));

		this->addChild(fish[i]);
		this->addChild(s_fish);
		fish[i]->addChild(fish_c[i]);
		s_fish->setVisible(false);
		s_fish->runAction(RepeatForever::create(fish_mate));

		fish_c[i]->runAction(RepeatForever::create(fish_mate->clone()));

		F_Phy = PhysicsBody::createCircle((220.0f), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(-20, -110));
		F_Phy->setDynamic(false);
		F_Phy->setCategoryBitmask((int)PhysicsCategory::Spider);
		F_Phy->setCollisionBitmask((int)PhysicsCategory::None);
		F_Phy->setContactTestBitmask((int)PhysicsCategory::Hero);
		F_Phy->setRotationEnable(false);
		fish[i]->setPhysicsBody(F_Phy);
		F_Phy->setGravityEnable(false);
	}

	button = Sprite::create("mogi/b.png");
	button->setAnchorPoint(Vec2(0.5f, 0.5f));
	button->setPosition(Vec2(1500, 720));

	this->addChild(button);

	button2 = Sprite::create("mogi/b2.png");
	button2->setAnchorPoint(Vec2::ZERO);
	button2->setVisible(false);
	button2->setPosition(Vec2(1500, 720));

	button->addChild(button2);
	AudioEngine::preload("mogi/sound/switch-12.mp3");

	B_Phy = PhysicsBody::createBox(button->getContentSize(), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
	B_Phy->setDynamic(false);
	B_Phy->setCategoryBitmask((int)PhysicsCategory::Button);
	B_Phy->setCollisionBitmask((int)PhysicsCategory::None);
	B_Phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	B_Phy->setRotationEnable(false);
	button->setPhysicsBody(B_Phy);
	B_Phy->setGravityEnable(false);

	water_Ani = Animation::create();
	water_Ani->setDelayPerUnit(0.25f);
	water_Ani->addSpriteFrameWithFile("mogi/Water2.png");
	water_Ani->addSpriteFrameWithFile("mogi/Water1.png");
	water_Ani->addSpriteFrameWithFile("mogi/Water2.png");
	water_Ani->addSpriteFrameWithFile("mogi/Water1.png");
	water_mate = Animate::create(water_Ani);

	Water_c = Sprite::create();
	Water = Sprite::create();
	S_Water = Sprite::create();

	Water_c->setAnchorPoint(Vec2::ZERO);
	Water->setContentSize(Size(100, 720));

	Water->setAnchorPoint(Vec2::ZERO);
	Water->setPosition(Vec2(1500, 720));

	this->addChild(Water);
	this->addChild(S_Water);
	Water->addChild(Water_c);

	S_Water->setVisible(false);
	S_Water->runAction(RepeatForever::create(water_mate));
	Water_c->runAction(RepeatForever::create(water_mate->clone()));

	W_Phy = PhysicsBody::createBox(Water->getContentSize(), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(60, 0));
	W_Phy->setDynamic(false);
	W_Phy->setCategoryBitmask((int)PhysicsCategory::Spider);
	W_Phy->setCollisionBitmask((int)PhysicsCategory::None);
	W_Phy->setContactTestBitmask((int)PhysicsCategory::Hero);
	W_Phy->setRotationEnable(false);
	Water->setPhysicsBody(W_Phy);
	W_Phy->setGravityEnable(false);

	suro[0] = Sprite::create("mogi/suro1.png");
	suro[1] = Sprite::create("mogi/suro2.png");
	for (int i = 0; i < 2; i++)
	{
		suro[i]->setAnchorPoint(Vec2::ZERO);
		suro[i]->setContentSize(Size(50, 360));
		suro[i]->setPosition(Vec2(1500, 720));

		this->addChild(suro[i]);

		S_Phy = PhysicsBody::createBox(suro[i]->getContentSize(), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(0, 0));
		S_Phy->setDynamic(false);
		S_Phy->setCategoryBitmask((int)PhysicsCategory::Spider);
		S_Phy->setCollisionBitmask((int)PhysicsCategory::None);
		S_Phy->setContactTestBitmask((int)PhysicsCategory::Hero);
		S_Phy->setRotationEnable(false);
		suro[i]->setPhysicsBody(S_Phy);
		S_Phy->setGravityEnable(false);
	}

	for (int i = 0; i < 5; i++)
	{
		Eat[i] = Sprite::create("mogi/eat.png");
		Eat[i]->setAnchorPoint(Vec2::ZERO);
		Eat[i]->setContentSize(Size(50, 50));
		Eat[i]->setPosition(Vec2(-120, 120));

		fish[random(0,4)]->addChild(Eat[i]);

		Eat_Phy = PhysicsBody::createBox(Eat[i]->getContentSize(), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(-8.5f, 0));
		Eat_Phy->setDynamic(false);
		Eat_Phy->setCategoryBitmask((int)PhysicsCategory::Eat);
		Eat_Phy->setCollisionBitmask((int)PhysicsCategory::None);
		Eat_Phy->setContactTestBitmask((int)PhysicsCategory::Hero);
		Eat_Phy->setRotationEnable(false);
		Eat[i]->setPhysicsBody(Eat_Phy);
		Eat_Phy->setGravityEnable(false);
	}
	for (int j = 5; j < 10; j++)
	{
		Eat[j] = Sprite::create("mogi/eat.png");
		Eat[j]->setAnchorPoint(Vec2::ZERO);
		Eat[j]->setContentSize(Size(50, 50));
		Eat[j]->setPosition(Vec2(185, 360));

		fish[random(0, 4)]->addChild(Eat[j]);

		Eat_Phy = PhysicsBody::createBox(Eat[j]->getContentSize(), PhysicsMaterial(0.0f, 0.0f, 0.0f), Vec2(-8.5f, 0));
		Eat_Phy->setDynamic(false);
		Eat_Phy->setCategoryBitmask((int)PhysicsCategory::Eat);
		Eat_Phy->setCollisionBitmask((int)PhysicsCategory::None);
		Eat_Phy->setContactTestBitmask((int)PhysicsCategory::Hero);
		Eat_Phy->setRotationEnable(false);
		Eat[j]->setPhysicsBody(Eat_Phy);
		Eat_Phy->setGravityEnable(false);
	}
	
	////Shake////----------------------------
	//shake = Shake::create(5, 100, 50);
	//this->runAction(shake);
	//---------------------------------------
	//body->runAction(Sequence::create(CallFunc::create(CC_CALLBACK_0(Mogi::Pt1, this)), CallFunc::create(CC_CALLBACK_0(Mogi::Pt2, this)), nullptr));


	Gameover_bg = Sprite::create("mogi/게임오버.png");
	Gameover_bg->setAnchorPoint(Vec2::ZERO);
	Gameover_bg->setContentSize(Size(950, 500));
	Gameover_bg->setPosition(Vec2(165,0));
	this->addChild(Gameover_bg);
	
	Gameover_bg->runAction(FadeOut::create(0));

	
	auto Gameover_main_2 = Sprite::create("mogi/메인메뉴.png");
	Gameover_main_2->setColor(Color3B(170, 170, 170));
	Gameover_main = MenuItemSprite::create(Sprite::create("mogi/메인메뉴.png"), Gameover_main_2, CC_CALLBACK_1(Mogi::Main_menu, this));
	Gameover_main->setPosition(-400,-220);

	auto Gameover_restart_2 = Sprite::create("mogi/다시하기.png");
	Gameover_restart_2->setColor(Color3B(170, 170, 170));
	Gameover_restart = MenuItemSprite::create(Sprite::create("mogi/다시하기.png"), Gameover_restart_2, CC_CALLBACK_1(Mogi::Main_restart, this));
	Gameover_restart->setPosition(80, -220);

	auto Gameover_menu = Menu::create(Gameover_main , Gameover_restart,NULL);
	Gameover_menu->setAnchorPoint(Vec2::ZERO);
	Gameover_main->runAction(FadeOut::create(0));
	Gameover_restart->runAction(FadeOut::create(0));
	Gameover_bg->addChild(Gameover_menu);

	three = Sprite::create("mogi/Three.png");
	two = Sprite::create("mogi/Two.png");
	one = Sprite::create("mogi/One.png");

	three->setPosition(Vec2(640,360));
	two->setPosition(Vec2(640,360));
	one->setPosition(Vec2(640,360));

	three->setVisible(false);
	two->setVisible(false);
	one->setVisible(false);

	this->addChild(three);
	this->addChild(two);
	this->addChild(one);

	Start_sp = Sprite::create("mogi/게임 시작.png");
	Start_sp->setVisible(false);
	Start_sp->setPosition(Vec2(640, 360));
	Start_sp->setScale(0.3);
	this->addChild(Start_sp);

	Food_L = Label::createWithSystemFont("", "Marker Felt.ttf", 50);
	Food_L->setPosition(Vec2(640, 650));
	this->addChild(Food_L);
}
void Mogi::M_1_Init()
{
	fish[0]->	setPosition(Vec2(1300, 0));
	gumijul[0]->setPosition(Vec2(1300 + 740, 576));
	fish[1]->	setPosition(Vec2(1300 + 1480, 0));
	gumijul[1]->setPosition(Vec2(1300 + 1480 + 370, 576));
	fish[2]->   setPosition(Vec2(1300 + 1480 + 740 + 370, 0));
	gumijul[2]->setPosition(Vec2(1300 + 1480 + 740 + 370, 576));
	button->	setPosition(Vec2(1300 + 1480 + 1480 + 370, 360));
	Water->		setPosition(Vec2(button->getPosition().x + 370, 0));
	fish[3]->	setPosition(Vec2(Water->getPosition().x + 370, 0));
	gumijul[3]->setPosition(Vec2(Water->getPosition().x + 370, 576));
	suro[0]->	setPosition(Size(Water->getPosition().x + 370 + 740, 0));
	suro[1]->	setPosition(Size(Water->getPosition().x + 370 + 740, 360));
	fish[4]->setPosition(Vec2(Water->getPosition().x + 370 + 1480, 0));
	gumijul[4]->setPosition(Vec2(Water->getPosition().x + 370 + 1480, 576));

	WaterOpen = random(1, 2);
	Pco = random(2,3);
}
	 
void Mogi::Main_menu(Ref* pSender)
{
	if (Gameover == true)
	{
		Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiMain::createScene(), Color3B(255, 255, 255)));
	}
}

void Mogi::Main_restart(Ref * pSender)
{
	if (Gameover == true)
	{
		Director::getInstance()->replaceScene(TransitionFade::create(2.0f, Mogi::createScene(), Color3B(255, 255, 255)));
	}
}

void Mogi::onKeyPressed(EventKeyboard::KeyCode keyCode, Event * event)
 {
	 switch(keyCode)
	 {
	 case EventKeyboard::KeyCode::KEY_G:
		 this->getScene()->getPhysicsWorld()->setDebugDrawMask(
			 this->getScene()->getPhysicsWorld()->getDebugDrawMask() == PhysicsWorld::DEBUGDRAW_ALL ?
			 PhysicsWorld::DEBUGDRAW_NONE : PhysicsWorld::DEBUGDRAW_ALL);
		 break;
	 case EventKeyboard::KeyCode::KEY_ESCAPE:
		 if (Ps == true)
		 {
			 Director::sharedDirector()->resume();
			 PsT = 3;
			 Ps = false;
		 }
		 else if (Ps == false)
		 {
			 Director::sharedDirector()->pause();
			 PsT = 3;
			 Ps = true;
		 }
		 break;
	 case EventKeyboard::KeyCode::KEY_SPACE:
		 if (updown == true) updown = false;
		 else if (updown == false) updown = true;

		 if (!GameStart)
		 {
			 GameStart = true;
		 }
		 break;
	 case EventKeyboard::KeyCode::KEY_P:
		 food = 50;
		 break;
	 }
 }
 void Mogi::onKeyReleased(EventKeyboard::KeyCode keyCode, Event * event)
 { 
	 switch (keyCode)
	 {
	  
	 }
 }
 void Mogi::update(float delta)
 {
	 static float tick_start = 0;
	 if (GameStart)
	 {
		 if (!start) tick_start += delta;

		 if (tick_start >= 1)
		 {
			 three->setVisible(true);
			 Sound_Scale += delta;
			 log("%f", Sound_Scale);
			 AudioEngine::setVolume(Mogi_bg, Sound_Scale);
			 if (sound == false)
			 {
				 Mogi_bg = AudioEngine::play2d("mogi/sound/게임진행.mp3" , true);//뒤에 true가 붙으면 무한재생
				 sound = true;
			 }
		 }
		 if (tick_start >= 2)
		 {
			 three->setVisible(false);
			 two->setVisible(true);
		 } 
		 if (tick_start >= 3)
		 {
			 one->setVisible(true);
			 two->setVisible(false);
		 }
		 if (tick_start >= 4)
		 {
			 one->setVisible(false);
			 Start_sp->setVisible(true);
		 }
		 if (tick_start >= 5)
		 {
			 Start_sp->setVisible(false);
			 start = true;
		 }
		 if (tick_start != 0 && start)
		 {
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
			 tick_start = 0;
		 }
	 }

	 AudioEngine::setVolume(Mogi_bg, Sound_Scale);
	 if (Sound_Scale > 0 && Gameover)
	 {
		 Sound_Scale -= delta;
		 log("%f", Sound_Scale);
	 }

	 if (start)
	 {
		 if (updown == true) body->getPhysicsBody()->setVelocity(Vec2(0, 450));
		 if (updown == false)body->getPhysicsBody()->setVelocity(Vec2(0, -450));

		 if (FoodTime < 0.5f)
		 {
			 FoodTime += delta;
		 }
		 CCLOG("%d", food);
		 if (food >= 50)
		 {
			 Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiM::createScene(), Color3B(0,0,0)));
			 food = -100;
		 }

		 if (body->getPosition().y - 32 < 0 && Gameover == false)
		 {
			 updown = true;
			 Gameover = true;
			 CCLOG("GAME OVER");
			 auto Blood_eff = ParticleSystemQuad::create("mogi/Effect/Blood.plist");
			 Blood_eff->setPosition(Vec2(body->getPosition().x, body->getPosition().y));
			 this->addChild(Blood_eff);
			 Gameover_bg->runAction(
				 Spawn::create(
					 MoveBy::create(1, Vec2(0, 110)),
					 FadeIn::create(1), nullptr));
			 Gameover_main->runAction(FadeIn::create(1));
			 Gameover_restart->runAction(FadeIn::create(1));
		 }
		 if (body->getPosition().y + 32 > 720 && Gameover == false)
		 {
			 updown = false;
			 Gameover = true;
			 CCLOG("GAME OVER");
			 auto Blood_eff = ParticleSystemQuad::create("mogi/Effect/Blood.plist");
			 Blood_eff->setPosition(Vec2(body->getPosition().x, body->getPosition().y));
			 this->addChild(Blood_eff);
			 Gameover_bg->runAction(
				 Spawn::create(
					 MoveBy::create(1, Vec2(0, 110)),
					 FadeIn::create(1), nullptr));
			 Gameover_main->runAction(FadeIn::create(1));
			 Gameover_restart->runAction(FadeIn::create(1));
		 }
		 for (int i = 0; i < 5; i++)
		 {
			 if (gumijul[i]->getPosition().x < 1000)
			 {
				 if (gumi[i]->getPosition().y < 10)
				 {
					 gumi[i]->getPhysicsBody()->setVelocity(Vec2(0, -200));
					 if (gumi[i]->getPosition().y < -96)
					 {
						 gumi[i]->getPhysicsBody()->setVelocity(Vec2(0, 0));
					 }
				 }
			 }
		 }

		 if (WaterOpen == 1 && suro[0]->getPosition().x < 1000)
		 {
			 suro[0]->getPhysicsBody()->setVelocity(Vec2(-400, -700));
		 }
		 if (WaterOpen == 2 && suro[1]->getPosition().x < 1000)
		 {
			 suro[1]->getPhysicsBody()->setVelocity(Vec2(-400, 700));
		 }

		 switch (Pco)
		 {
		 case 1:
			 pt1();
			 M_1_Init();

			 break;
		 case 2:

			 break;
		 case 3:

			 break;
		 }
		 if (Gameover == true)
		 {
			 updown = false;
			 Gameover_rotate += 18;
		 }
		 Food_L->setString(StringUtils::format("얻은 체취 : %d / 5", food / 10));

	 }
	 body->setRotation(Gameover_rotate);
 }

 void Mogi::pt1()
 {
	 for (int i = 0; i < 5; i++)
	 {
		 gumijul[i]->getPhysicsBody()->setVelocity(Vec2(-400, 0));
		 fish	[i]->getPhysicsBody()->setVelocity(Vec2(-400, 0));
	 }
	
	 button	   ->getPhysicsBody()->setVelocity(Vec2(-400, 0));
	 Water	   ->getPhysicsBody()->setVelocity(Vec2(-400, 0));
	 for (int j = 0; j < 2; j++)
	 {
		 suro[j]->getPhysicsBody()->setVelocity(Vec2(-400, 0));
	 }
 }
 void Mogi::pt2()
 {
	
 }
 void Mogi::pt3()
 {
	
 }
 bool Mogi::onContactBegin(PhysicsContact & contact)
 {
	 Node* nodeSpider = nullptr;
	 Node* nodeButton = nullptr;
	 Node* nodeHero = nullptr;
	 Node* nodeEat = nullptr;

	 PhysicsShape* shape[2] = { contact.getShapeA(), contact.getShapeB() };

	 for (int i = 0; i < 2; i++)
	 {
		 switch (shape[i]->getCategoryBitmask())
		 {
		 case (int)PhysicsCategory::Spider:
		 {
			 nodeSpider = shape[i]->getBody()->getNode();
			 break;
		 }
		 case (int)PhysicsCategory::Hero:
		 {
			 nodeHero = shape[i]->getBody()->getNode();
			 break;
		 }
		 case (int)PhysicsCategory::Button:
		 {
			 nodeButton = shape[i]->getBody()->getNode();
			 break;
		 }
		 case (int)PhysicsCategory::Eat:
		 {
			 nodeEat = shape[i]->getBody()->getNode();
			 break;
		 }
		 }
	 }

	 if (nodeHero != nullptr&&Gameover == false)
	 {
		 if (nodeSpider!= nullptr)
		 {
			 Gameover = true;
			 auto Blood_eff = ParticleSystemQuad::create("mogi/Effect/Blood.plist");
			 Blood_eff->setPosition(Vec2(body->getPosition().x, body->getPosition().y));
			 this->addChild(Blood_eff);
			 Gameover_bg->runAction(
				 Spawn::create(
					 MoveBy::create(1,Vec2( 0, 110)),
					 FadeIn::create(1),nullptr));
			 Gameover_main->runAction(FadeIn::create(1));
			 Gameover_restart->runAction(FadeIn::create(1));
			 mogi_Hit_sound = random(1, 3);
			 switch (mogi_Hit_sound)
			 {
			 case 1:
				 mogi_Hit_sound_1 = AudioEngine::play2d("mogi/sound/Hit1.mp3");
				 break;
			 case 2:
				 mogi_Hit_sound_2 = AudioEngine::play2d("mogi/sound/Hit2.mp3");

				 break;
			 case 3:
				 mogi_Hit_sound_3 = AudioEngine::play2d("mogi/sound/Hit3.mp3");

				 break;
			 default:
				 break;
			 }
		 }
		 if (nodeButton != nullptr)
		 {
			 int Button_s = AudioEngine::play2d("mogi/sound/switch-12.mp3");
			 Water->getPhysicsBody()->setVelocity(Vec2(-400,-650));
			 button2->setVisible(true);
		 }
		 if (nodeEat != nullptr && FoodTime >= 0.5f)
		 {
			 CCLOG("너무 맛있당");

			 food += 10;
			 FoodTime = 0;
		 }
		 if (nodeEat != nullptr)
		 {
			 nodeEat->runAction(Place::create(Vec2(2000, 2000)));
		 }
	 }
	 return true;
 }

 void Mogi::onMouseMove(cocos2d::Event* event)
 {
	/* auto mouseEvent = static_cast<EventMouse*>(event);

	 CCLOG("X : %d		",getCursorX);
	 CCLOG("Y : %d",getCursorY);*/
 }
