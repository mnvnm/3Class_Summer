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

Scene* MogiM::createScene()
{
	auto scene	=	Scene::createWithPhysics(); // 물리에서 중요 //
	auto layer	= MogiM::create();

	scene->addChild(layer);
	//물리적 중력 설정및 디버그 Draw
	scene->getPhysicsWorld()->setGravity(Vec2(0, -1000.0f));//물리적 중력
	scene->getPhysicsWorld()->setDebugDrawMask(PhysicsWorld::DEBUGDRAW_ALL);//디버그 Draw

	return scene;
}

bool MogiM::init()
{
	if (!LayerColor::initWithColor(Color4B::BLACK))
	{
		return false;
	}
	this->scheduleUpdate();

	this->setKeyboardEnabled(true);

	this->setTouchEnabled(true);
	//////////////////////////////마우스 이벤트 /////////////////////////

	//EventListenerMouse* mouse = EventListenerMouse::create();
	//mouse->onMouseMove = CC_CALLBACK_1(Mogi::onMouseMove, this); 
	//_eventDispatcher->addEventListenerWithSceneGraphPriority(mouse, this);

	///////////////////////////////충돌 이벤트 /////////////////////////////
	//auto contactListener = EventListenerPhysicsContact::create();
	//contactListener->onContactBegin = CC_CALLBACK_1(MogiM::onContactBegin, this);
	//_eventDispatcher->addEventListenerWithSceneGraphPriority(contactListener, this);

	AudioEngine::stopAll();
	int bgm = AudioEngine::play2d("mogi/sound/선택.mp3", true);//뒤에 true가 붙으면 무한재생

	AudioEngine::preload("mogi/sound/단체박수1.mp3");

	Baby = Sprite::create("mogi/Body.png");
	Baby->setContentSize(Size(1280, 720));
	Baby->setAnchorPoint(Vec2::ZERO);
	Baby->setPosition(Vec2(0,0));
	this->addChild(Baby);

	Head = Sprite::create();
	Head->setContentSize(Size(100,100));
	Head->setAnchorPoint(Vec2::ZERO);
	Head->setPosition(Vec2(677, 580));
	this->addChild(Head, 4);

	Arm = Sprite::create();
	Arm->setContentSize(Size(100, 100));
	Arm->setAnchorPoint(Vec2::ZERO);
	Arm->setPosition(Vec2(795, 50));
	this->addChild(Arm , 3 );

	Leg = Sprite::create();
	Leg->setContentSize(Size(100, 100));
	Leg->setAnchorPoint(Vec2::ZERO);
	Leg->setPosition(Vec2(345, 44));
	this->addChild(Leg , 2);

	Check = Sprite::create("mogi/Blood/Check_Body.png");
	Check->setContentSize(Size(140, 140));
	Check->setAnchorPoint(Vec2::ZERO);
	Check->setPosition(Vec2(775, 30));
	this->addChild(Check , 10);
	for (int i = 0; i < 3; i++)
	{
		Check_X[i] = Sprite::create("mogi/Blood/X.png");
		Check_X[i]->setContentSize(Size(140, 140));
		Check_X[i]->setAnchorPoint(Vec2::ZERO);
		Check_X[i]->setVisible(false);
		this->addChild(Check_X[i] , 50);
	}
	Check_X[0]->setPosition(Vec2(657,560));
	Check_X[1]->setPosition(Vec2(775,30));
	Check_X[2]->setPosition(Vec2(325,24));

	
	// 메모리에(만) 저장하기///////////////////////////////////////////
	//for (int i = 0; i < 3; i++) // 값을 0으로 설정함 --------------------------- >↘
	//{
	//	Configuration::getInstance()->setValue(StringUtils::format("Body%d", i), Value(0));
	//}
	
	//텍스트로 저장하기/////////////////////////////////////
	//auto rootPath = FileUtils::getInstance()->getWritablePath();
	//rootPath.append("/BodyChoose.txt");
	//쓰기/////////////////////////////////////////////
	//FILE* fp;
	//fopen_s(&fp, rootPath.c_str(), "w");
	//for (int i = 0; i < 3; i++)
	//{
	//	fputc(f_body[i] , fp);
	//	fputc(',', fp);
	//}
	//fclose(fp);
	//불러오기/////////////////////////////////////////
	//fopen_s(&fp, rootPath.c_str(), "r");
	//fscanf(fp, "%d,%d,%d", &f_body[0], &f_body[1], &f_body[2]);
	//fclose(fp);

	GameClear_bg = Sprite::create("mogi/클리어.png");
	GameClear_bg->setAnchorPoint(Vec2::ZERO);
	GameClear_bg->setContentSize(Size(950, 500));
	GameClear_bg->setPosition(Vec2(165, 0));
	this->addChild(GameClear_bg);

	/*auto firework_1 = ParticleSystemQuad::create("mogi/Effect/firework.plist");
	firework_1->setPosition(Vec2(random(150, 1280 - 150), random(110, 610)));
	this->addChild(firework_1);*/


	GameClear_bg->runAction(FadeOut::create(0));


	auto GameClear_main_2 = Sprite::create("mogi/메인메뉴.png");
	GameClear_main_2->setColor(Color3B(170, 170, 170));
	GameClear_main = MenuItemSprite::create(Sprite::create("mogi/메인메뉴.png"), GameClear_main_2, CC_CALLBACK_1(MogiM::Main_menu, this));
	GameClear_main->setPosition(-400, -220);

	auto GameClear_next_2 = Sprite::create("mogi/다음 스테이지.png");
	GameClear_next_2->setColor(Color3B(170, 170, 170));
	GameClear_next = MenuItemSprite::create(Sprite::create("mogi/다음 스테이지.png"), GameClear_next_2, CC_CALLBACK_1(MogiM::Main_Next, this));
	GameClear_next->setPosition(80, -220);

	auto GameClear_menu = Menu::create(GameClear_main, GameClear_next, NULL);
	GameClear_menu->setAnchorPoint(Vec2::ZERO);
	GameClear_main->runAction(FadeOut::create(0));
	GameClear_next->runAction(FadeOut::create(0));
	GameClear_bg->addChild(GameClear_menu);
}

void MogiM::Main_menu(Ref* pSender)
{
	if (GameClear == true)
	{
		Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiMain::createScene(), Color3B(255, 255, 255)));
	}
}

void MogiM::Main_Next(Ref * pSender)
{
	if (GameClear == true)
	{
		Director::getInstance()->replaceScene(TransitionFade::create(2.0f, MogiMain::createScene(), Color3B(255, 255, 255)));
	}
}
	
 void MogiM::onKeyPressed(EventKeyboard::KeyCode keyCode, Event * event)
 {
	 switch (keyCode)
	 {
	 case EventKeyboard::KeyCode::KEY_SPACE:
		 CheckBool = true;
		 break;
		 case EventKeyboard::KeyCode::KEY_P:
		 Director::getInstance()->replaceScene(TransitionFade::create(2.0f, Blood::createScene(), Color3B(255, 255, 255)));
		 break;
		 case EventKeyboard::KeyCode::KEY_O:
			 Configuration::getInstance()->setValue("Body0", Value(2));
			 Configuration::getInstance()->setValue("Body1", Value(2));
			 Configuration::getInstance()->setValue("Body2", Value(2));
			 break;

	 }
 }
 void MogiM::onKeyReleased(EventKeyboard::KeyCode keyCode, Event * event)
 {
	 switch (keyCode)
	 {
	 }
 }

 void MogiM::update(float delta)
 {
	//불러오기
	 f_body[0] = Configuration::getInstance()->getValue("Body0").asInt();//머리
	 f_body[1] = Configuration::getInstance()->getValue("Body1").asInt();//팔
	 f_body[2] = Configuration::getInstance()->getValue("Body2").asInt();//다리

	 switch (CheckCase)
	 {

	 case 1: //머리
		 if (CheckBool == false)
		 {
			 Check->runAction(Sequence::create(
				 DelayTime::create(0.2f),
				 CallFunc::create([=]() {
					 if (CheckBool == false && f_body[1] == 0)
					 {
						 Check->setPosition(Vec2(Arm->getPositionX() - 20, Arm->getPositionY() - 20));
						 CheckCase = 2;
					 }
					 else if (CheckBool == false && f_body[2] == 0)
					 {
						 Check->setPosition(Vec2(Leg->getPositionX() - 20, Leg->getPositionY() - 20));
						 CheckCase = 3;
					 }
					 else if (CheckBool == false && f_body[0] == 0)
					 {
						 Check->setPosition(Vec2(Head->getPositionX() - 20, Head->getPositionY() - 20));
						 Check->stopAllActions();
						 CheckCase = 1;
					 }}),nullptr));
		 }

		 break;
	 case 2: //팔

		 if (CheckBool == false)
		 {
			 Check->runAction(Sequence::create(
				 DelayTime::create(0.2f),
				 CallFunc::create([=]() {
					 if (CheckBool == false && f_body[2] == 0)
					 {
						 Check->setPosition(Vec2(Leg->getPositionX() - 20, Leg->getPositionY() - 20));
						 CheckCase = 3;
					 }
					 else if (CheckBool == false && f_body[0] == 0)
					 {
						 Check->setPosition(Vec2(Head->getPositionX() - 20, Head->getPositionY() - 20));
						 CheckCase = 1;
					 }
					 else if (CheckBool == false && f_body[1] == 0)
					 {
						 Check->setPosition(Vec2(Arm->getPositionX() - 20, Arm->getPositionY() - 20));
						 Check->stopAllActions();
						 CheckCase = 2;
					 }}),nullptr));
		 }
		 break;
	 case 3: //다리

		 if (CheckBool == false)
		 {
			 Check->runAction(Sequence::create(
				 DelayTime::create(0.2f),
				 CallFunc::create([=]() {
					 if (CheckBool == false && f_body[0] == 0)
					 {
						 Check->setPosition(Vec2(Head->getPositionX() - 20, Head->getPositionY() - 20));
						 CheckCase = 1;
					 }
					 else if (CheckBool == false && f_body[1] == 0)
					 {
						 Check->setPosition(Vec2(Arm->getPositionX() - 20, Arm->getPositionY() - 20));
						 CheckCase = 2;
					 }
					 else if (CheckBool == false && f_body[2] == 0)
					 {
						 Check->setPosition(Vec2(Leg->getPositionX() - 20, Leg->getPositionY() - 20));
						 Check->stopAllActions();
						 CheckCase = 3;
					 }}),nullptr));
		 }
		 break;
	 default:
		 break;
	 }

	 if (CheckBool == true && CheckCase == 1 && f_body[0] == 0 && replace == false)
	 {
		 replace = true;
		 Configuration::getInstance()->setValue("Body0", Value(1));
		 Director::getInstance()->replaceScene(TransitionFade::create(2.0f, Blood::createScene(), Color3B(255, 255, 255)));
	 }

	 if (CheckBool == true && CheckCase == 2 && f_body[1] == 0 && replace == false)
	 {
		 replace = true;
		 Configuration::getInstance()->setValue("Body1", Value(1));
		 Director::getInstance()->replaceScene(TransitionFade::create(2.0f, Blood::createScene(), Color3B(255, 255, 255)));
	 }

	 if (CheckBool == true && CheckCase == 3 && f_body[2] == 0 && replace == false)
	 {
		 replace = true;
		 Configuration::getInstance()->setValue("Body2", Value(1));
		 Director::getInstance()->replaceScene(TransitionFade::create(2.0f, Blood::createScene(), Color3B(255, 255, 255)));
	 }
	 if (f_body[0] == 2)
	 {
		 Check_X[0]->setVisible(true);
	 }
	 if (f_body[1] == 2)
	 {
		 Check_X[1]->setVisible(true);
	 }
	 if (f_body[2] == 2)
	 {
		 Check_X[2]->setVisible(true);
	 }

	 if (f_body[0] == 2 && f_body[1] == 2 && f_body[2] == 2 && replace == false && GameClear == false)
	 {
		 replace = true;
		 GameClear = true;
		 CCLOG("GAME CLEAR");
		 GameClear_bg->runAction(
			 Spawn::create(
				 MoveBy::create(1.0f, Vec2(0, 110)),
				 FadeIn::create(1.0f), 
				 CallFunc::create([=]() { 
		 GameClear_main->runAction(FadeIn::create(1.0f));
		 GameClear_next->runAction(FadeIn::create(1.0f)); }),
				 nullptr));
		 Clear_sound = AudioEngine::play2d("mogi/sound/단체박수1.mp3", false , 0.6);

		runAction(
			 RepeatForever::create(Sequence::create(
			 CallFunc::create([=]() {
			 auto firework_1 = ParticleSystemQuad::create("mogi/Effect/firework.plist");
			 firework_1->setPosition(Vec2(random(150 , 1280 - 150), random(110, 610)));
			 this->addChild(firework_1); }),
				 DelayTime::create(0.125f),
			 CallFunc::create([=]() {
				 auto firework_2 = ParticleSystemQuad::create("mogi/Effect/firework_2.plist");
				 firework_2->setPosition(Vec2(Vec2(random(150, 1280 - 150), random(110, 610))));
				 this->addChild(firework_2); }),
				 DelayTime::create(0.4f),
				 CallFunc::create([=]() {
					 auto firework_3 = ParticleSystemQuad::create("mogi/Effect/firework_3.plist");
					 firework_3->setPosition(Vec2(Vec2(random(150, 1280 - 150), random(110, 610))));
					 this->addChild(firework_3); }),
					 DelayTime::create(0.25f),
					 CallFunc::create([=]() {
						 auto firework_4 = ParticleSystemQuad::create("mogi/Effect/firework_4.plist");
						 firework_4->setPosition(Vec2(Vec2(random(150, 1280 - 150), random(110, 610))));
						 this->addChild(firework_4); }),
						 DelayTime::create(0.25f), nullptr)));

		 Check->setVisible(false);
		 Check->runAction(
			 CallFunc::create([=]() 
				 {
				 Configuration::getInstance()->setValue("Body0", Value(0)); 
				 Configuration::getInstance()->setValue("Body1", Value(0));
				 Configuration::getInstance()->setValue("Body2", Value(0));
				 }));
	 }
 }

 void MogiM::onTouchesBegan(const std::vector<Touch*>& touches, Event * unused_event)
 {
	 Touch* t = nullptr;
	 for (std::vector<Touch*>::const_iterator it = touches.begin(); it != touches.end(); it++)
	 {
		 t = (Touch*)*it;

		 CCLOG("X : %f		Y : %f", t->getLocation().x, t->getLocation().y);
	 }
 }

