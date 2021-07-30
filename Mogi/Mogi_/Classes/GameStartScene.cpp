#include "HelloWorldScene.h"
#include "TEST.h"
#include "MenuScene.h"'
#include "BangScene.h"
#include "GameStartScene.h"

#include "audio/include/SimpleAudioEngine.h"

using namespace CocosDenshion;

#pragma execution_character_set("utf-8")

USING_NS_CC;


int GameStatus = 0;

float BTime = 0;
float BCount;

Scene* GameStartScene::createScene()
{
	auto scene = Scene::create();
	auto layer = GameStartScene::create();

	scene->addChild(layer);

	return scene;
}

static void problemLoading(const char* filename)
{
    printf("Error while loading: %s\n", filename);
    printf("Depending on how you compiled you might have to add 'Resources/' in front of filenames in HelloWorldScene.cpp\n");
}

bool GameStartScene::init()
{
    if ( !LayerColor::initWithColor(Color4B(255,255,255,255)) )
    {
        return false;
    }

	this->scheduleUpdate();
	this->setTouchEnabled(true);

    auto visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

	GameInfo = LayerColor::create
	(Color4B(240, 240, 240, 255), 320, 50);
	this->addChild(GameInfo);

	auto home = MenuItemImage::create("res/Bang/back.png", "res/Bang/backSel.png", CC_CALLBACK_1(GameStartScene::Home, this));
	auto info = MenuItemImage::create("res/Bang/info.png", "res/Bang/infoSel.png");
	auto board = MenuItemImage::create("res/Bang/scoreBoardDivide.png", "res/Bang/scoreBoardDivide.png");
	MenuItem* Gamebotton = MenuItemImage::create("res/Bang/gameButton.png", "res/Bang/gameButtonsSel.png", CC_CALLBACK_1(GameStartScene::Start, this));
	GameInfo->addChild(board);

	Gamebotton->setName("GameStart");

	Gamebotton->setPosition(Vec2(0,-180));
	board->setPosition(Vec2(320 / 2, 25));
	home->setAnchorPoint(Vec2::ZERO);
	home->setPosition(Vec2(-160, -237));
	info->setAnchorPoint(Vec2::ZERO);
	info->setPosition(Vec2(115, -237));
	auto GameMenu = Menu::create(home, info, Gamebotton, NULL);

	//GameMenu->alignItemsHorizontally();//alignItemsHorizontally 수평  /alignItemsVirtically 수직
	//GameMenu->alignItemsHorizontallyWithPadding(80);
	//GameMenu->setPosition(Vec2(320 / 2, 25));

	spr_temp = Sprite::create();
	this->addChild(spr_temp);
	spr_temp->setPosition(Vec2(320,480));
	//시작할때 준비
	auto GameCache = SpriteFrameCache::getInstance();
	GameCache->addSpriteFramesWithFile("res/Bang/fidget_1.plist");
	
	Walk = Animation::create();
	for (int i = 1; i <= 10; i++)
	{
		Walk->addSpriteFrame(GameCache->getSpriteFrameByName(StringUtils::format("fidget_1_%04d.png", i)));
	}
	Walk->setDelayPerUnit(0.045f);
	ani_walk = Animate::create(Walk);

	spr_temp->setVisible(false);
	spr_temp->runAction(RepeatForever::create(ani_walk));

	spr_temp1 = Sprite::create();
	this->addChild(spr_temp1);
	spr_temp1->setPosition(Vec2(320, 480));

	GameCache->addSpriteFramesWithFile("res/Bang/shoot_1.plist");
	//총 쏘기
	Shoot = Animation::create();
	for (int i = 1; i <= 47; i++)
	{
		Shoot->addSpriteFrame(GameCache->getSpriteFrameByName(StringUtils::format("shoot_1_%04d.png",i)));
	}
	Shoot->setDelayPerUnit(0.05f);

	ani_Bang = Animate::create(Shoot);

	spr_temp1->setVisible(false);
	spr_temp1->runAction(RepeatForever::create(ani_Bang));

	spr_temp2 = Sprite::create();
	this->addChild(spr_temp2);
	spr_temp2->setPosition(Vec2(320, 480));
	//잘못 쏴서 쫄음
	GameCache->addSpriteFramesWithFile("res/Bang/coward_1.plist");
	
	Coward = Animation::create();
	for (int i = 1; i <= 12; i++)
	{
		Coward->addSpriteFrame(GameCache->getSpriteFrameByName(StringUtils::format("coward_1_%04d.png", i)));
	}
	Coward->setDelayPerUnit(0.05f);

	ani_Coward = Animate::create(Coward);

	spr_temp2->setVisible(false);
	spr_temp2->runAction(RepeatForever::create(ani_Coward));
	//이겨서 건슬링
	spr_temp3 = Sprite::create();
	this->addChild(spr_temp3);
	spr_temp3->setPosition(Vec2(320, 480));
	
	GameCache->addSpriteFramesWithFile("res/Bang/gunsling_1.plist");

	Gunsling = Animation::create();
	for (int i = 1; i <= 36; i++)
	{
		Gunsling->addSpriteFrame(GameCache->getSpriteFrameByName(StringUtils::format("gunsling_1_%04d.png", i)));
	}
	Gunsling->setDelayPerUnit(0.05f);

	ani_Gunsling = Animate::create(Gunsling);

	spr_temp3->setVisible(false);
	spr_temp3->runAction(RepeatForever::create(ani_Gunsling));
	//총알 미쓰
	spr_temp4 = Sprite::create();
	this->addChild(spr_temp4);
	spr_temp4->setPosition(Vec2(320,480)/2);

	GameCache->addSpriteFramesWithFile("res/Bang/miss_2.plist");

	Miss = Animation::create();
	for (int i = 1; i <= 21; i++)
	{
		Miss->addSpriteFrame(GameCache->getSpriteFrameByName(StringUtils::format("miss_2_%04d.png", i)));
	}
	Miss->setDelayPerUnit(0.05f);

	ani_Miss = Animate::create(Miss);

	spr_temp4->setVisible(false);
	spr_temp4->runAction(RepeatForever::create(ani_Miss));
	//죽음

	death_temp = Sprite::create();
	this->addChild(death_temp);
	death_temp->setPosition(Vec2(320, 480) / 2);

	GameCache->addSpriteFramesWithFile("res/Bang/death_1.plist");

	Death = Animation::create();
	for (int j = 1; j <= 40; j++)
	{
		Death->addSpriteFrame(GameCache->getSpriteFrameByName(StringUtils::format("death_1_%04d.png", j)));
	}
	Death->setDelayPerUnit(0.05f);

	ani_Death = Animate::create(Death);


	death_temp->setVisible(false);
	death_temp->runAction(RepeatForever::create(ani_Death));
	//게임 시작 화면
	GameInfo->addChild(GameMenu);

	auto GameinfoText = Label::create("나", "", 12);
	GameinfoText->setPosition(Vec2(78,13));
	GameinfoText->setColor(Color3B(0, 0, 0));
	board->addChild(GameinfoText);

	auto GameinfoText2 = Label::create("상대", "", 12);
	GameinfoText2->setPosition(Vec2(78, 30));
	GameinfoText2->setColor(Color3B(0, 0, 0));
	board->addChild(GameinfoText2);

	auto bottonText = Label::create("START", "res/Bang/uni05_53.ttf", 20);
	bottonText->setPosition(Gamebotton->getContentSize() / 2);
	Gamebotton->addChild(bottonText);

	SpriteCom = Sprite::create("res/Bang/stand_2.png");
	SpritePlayer = Sprite::create("res/Bang/stand_2.png");

	SpriteCom->setPosition(Vec2(165, 400));
	SpritePlayer->setPosition(Vec2(165, 120));

	SpritePlayer->setFlipY(true);

	this->addChild(SpriteCom);
	this->addChild(SpritePlayer);

	game = Sprite::create();
	game->setPosition(Vec2(320, 480) / 2);
	this->addChild(game);

	auto AudioCache = SimpleAudioEngine::getInstance();
	AudioCache->preloadEffect("res/Bang/ready.wav");
	AudioCache->preloadEffect("res/Bang/steady.wav");
	AudioCache->preloadEffect("res/Bang/rsbang.wav");
	AudioCache->preloadEffect("res/Bang/miss_1.wav");
	AudioCache->preloadEffect("res/Bang/death_1.wav");
	AudioCache->preloadEffect("res/Bang/bang_1.wav");
	return true;
}

void GameStartScene::Home(Ref* pSender)
{
	Director::getInstance()->replaceScene(MenuScene::createScene());
}
void GameStartScene::Start(Ref* pSender)
{
	if (((MenuItem*)pSender)->getName().compare("GameStart") == 0)
	{
		SpritePlayer->stopAllActions();
		SpriteCom->stopAllActions();
		GameStatus = 1;

		BTime = 0;
		BCount = random(0.2f, 0.8f);
		bool NPCbang = true;
		BullCount = true;
		

		GameInfo->runAction(MoveTo::create(0.5f, Vec2(0, -200)));

		SpritePlayer->setPosition(Vec2(160, 120));
		SpritePlayer->runAction(ani_walk->clone());

		SpriteCom->setPosition(Vec2(160, 400));
		SpriteCom->runAction(Sequence::create(
			ani_walk->clone(),
			CallFunc::create(CC_CALLBACK_0(GameStartScene::Ready, this)),
			nullptr)
		);

		
	}
}
void GameStartScene::Ready()
{
	game->setSpriteFrame(Sprite::create("res/Bang/ready.png")->getSpriteFrame());

	game->setOpacity(0);

	game->runAction(
		Sequence::create
		(
			DelayTime::create(0.5f),
			CallFunc::create([=]() {
				SimpleAudioEngine::getInstance()->playEffect("res/Bang/ready.wav");
			}),
			FadeIn::create(0.5f),
			DelayTime::create(2.0f),
			FadeOut::create(0.5f), 
			CallFunc::create(CC_CALLBACK_0(GameStartScene::Steady, this)),
			NULL)
	);
}
void GameStartScene::Steady()
{
	game->setSpriteFrame(Sprite::create("res/Bang/steady.png")->getSpriteFrame());

	game->setOpacity(0);

	game->runAction(
		Sequence::create
		(
			DelayTime::create(0.5f),
			CallFunc::create([=]() {
				SimpleAudioEngine::getInstance()->playEffect("res/Bang/steady.wav");
			}),
			FadeIn::create(0.5f),
			DelayTime::create(2.0f),
			FadeOut::create(0.5f),
			CallFunc::create(CC_CALLBACK_0(GameStartScene::Bang, this)),
			NULL)
	);
}
void GameStartScene::Bang()
{
	float BANG;
	BANG = random(0.8f, 10.0f);
	

	game->setSpriteFrame(Sprite::create("res/Bang/bang.png")->getSpriteFrame());

	game->setOpacity(0);

	game->runAction(
		Sequence::create
		(
			DelayTime::create(BANG),
			CallFunc::create([=]() {
				GameStatus = 2;
				NPCbang = false;
				SimpleAudioEngine::getInstance()->playEffect("res/Bang/rsbang.wav");
			}),
			FadeIn::create(0),
			DelayTime::create(1.0f),
			FadeOut::create(0),
			nullptr)
	);
	CCLOG("%f", BCount);
	
}

void GameStartScene::update(float delta)
{
	if (NPCbang == false)
	{
		if (GameStatus == 2)
		{
			BTime += delta;

			if (BTime >= BCount)
			{
				NPCbang = true;
				CCLOG("%f", BCount);
				SpritePlayer->stopAllActions();
				SpriteCom->stopAllActions();
				SpriteCom->runAction(Sequence::create(
				ani_Bang->clone(), 
				ani_Gunsling->clone(), 
				DelayTime::create(1.0f),
				CallFunc::create([=]() { GameInfo->runAction(MoveTo::create(0.5f, Vec2(0, 0))); }),
				nullptr));
				SimpleAudioEngine::getInstance()->playEffect("res/Bang/bang_1.wav");
				SpritePlayer->runAction(ani_Death->clone());
				SimpleAudioEngine::getInstance()->playEffect("res/Bang/death_1.wav");

				BullCount = false;
			}
		}
	}
	
}

void GameStartScene::onTouchesBegan(const std::vector<Touch*>& touches, Event * unused_event)
{
	if (GameStatus == 1) // Bang 전
	{
		GameStatus = 3;
	}  
	if (GameStatus == 2) // Bang후
	{
		GameStatus = 4;
	}
	if (BullCount == true)
	{
		if (GameStatus == 3)//Bang 전 총을 쐈을 때,후에 쐈을 때
		{
			auto temp_spr = Sprite::create();
			temp_spr->setPosition(Vec2(random(0, 320), random(160, 480)));
			temp_spr->runAction(ani_Miss->clone());
			this->addChild(temp_spr);
			SimpleAudioEngine::getInstance()->playEffect("res/Bang/miss_1.wav");
			SpriteCom->stopAllActions();
			SpritePlayer->runAction(Sequence::create(ani_Bang->clone(), ani_Coward->clone(),nullptr));

			BullCount = false;
		}
		if (GameStatus == 4)
		{
			NPCbang = true;
			SimpleAudioEngine::getInstance()->playEffect("res/Bang/bang_1.wav");
			SpritePlayer->stopAllActions();
			SpritePlayer->runAction(Sequence::create(ani_Bang->clone(), ani_Gunsling->clone(), nullptr));
			SpriteCom->stopAllActions();
			SpriteCom->runAction(Sequence::create(ani_Death->clone(), DelayTime::create(1.0f), 
			CallFunc::create([=]() { GameInfo->runAction(MoveTo::create(0.5f, Vec2(0, 0))); }), nullptr));
			SimpleAudioEngine::getInstance()->playEffect("res/Bang/death_1.wav");

			CCLOG("%f", BTime);
			BullCount = false;
		}
	}
}

void GameStartScene::onTouchesEnded(const std::vector<Touch*>& touches, Event * unused_event)
{
}

void GameStartScene::onTouchesCancelled(const std::vector<Touch*>& touches, Event * unused_event)
{
}

void GameStartScene::onTouchesMoved(const std::vector<Touch*>& touches, Event * unused_event)
{
}
