#include "HelloWorldScene.h"
#include "TEST.h"
#include "MenuScene.h"'
#include "BangScene.h"
#include "GameStartScene.h"

#include "SimpleAudioEngine.h"

#pragma execution_character_set("utf-8")

USING_NS_CC;

Scene* MenuScene::createScene()
{
	auto scene = Scene::create();
	auto layer = MenuScene::create();

	scene->addChild(layer);

	return scene;
}

static void problemLoading(const char* filename)
{
    printf("Error while loading: %s\n", filename);
    printf("Depending on how you compiled you might have to add 'Resources/' in front of filenames in HelloWorldScene.cpp\n");
}

bool MenuScene::init()
{
    if ( !LayerColor::initWithColor(Color4B(255,255,255,255)) )
    {
        return false;
    }

	this->scheduleUpdate();

    auto visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

	MenuItem* item1 = MenuItemImage::create		("res/Bang/menuButtonBg.png", "res/Bang/menuButtonBgSel.png", CC_CALLBACK_1(MenuScene::Func1, this));
	MenuItem* item2 = MenuItemImage::create		("res/Bang/menuButtonBg.png", "res/Bang/menuButtonBgSel.png", CC_CALLBACK_1(MenuScene::Func1, this));
	MenuItemImage* item3 = MenuItemImage::create("res/Bang/menuButtonBg.png", "res/Bang/menuButtonBgSel.png");
	MenuItemImage* item4 = MenuItemImage::create("res/Bang/menuButtonBg.png", "res/Bang/menuButtonBgSel.png");
	MenuItemImage* item5 = MenuItemImage::create("res/Bang/menuButtonBg.png", "res/Bang/menuButtonBgSel.png");
	item1->setPosition(Vec2(320 / 2, 410)); 

	item2->setPosition(Vec2(320 / 2, 360));

	item3->setPosition(Vec2(320 / 2, 310));
	item3->getNormalImage()->setOpacity(200);
	item3->getSelectedImage()->setOpacity(200);

	item4->setPosition(Vec2(320 / 2, 260));
	item4->getNormalImage()->setOpacity(160);
	item4->getSelectedImage()->setOpacity(160);

	item5->setPosition(Vec2(320 / 2, 210));
	item5->getNormalImage()->setOpacity(120);
	item5->getSelectedImage()->setOpacity(120);
	
	auto item11 = Label::create("2 플레이어", "", 32);
	item11->setPosition(item1->getContentSize() / 2);
	item1->addChild(item11);
	auto item22 = Label::create("1 플레이어", "", 32);
	item22->setPosition(item2->getContentSize() / 2);
	item2->addChild(item22);
	auto item33 = Label::create("킬 갤러리", "", 32);
	item33->setPosition(item3->getContentSize() / 2);
	item3->addChild(item33);
	auto item44 = Label::create("더 많은 게임", "", 32);
	item44->setPosition(item4->getContentSize() / 2);
	item4->addChild(item44);
	auto item55 = Label::create("ready steady play", "res/Bang/uni05_53.ttf", 18);
	item55->setPosition(item5->getContentSize() / 2);
	item5->addChild(item55);
	auto itemText = Label::create("ready steady bang", "res/Bang/uni05_53.ttf", 26);
	
	itemText->setColor(Color3B(0,0,0));
	itemText -> setPosition(Vec2(320 / 2, 460));
	this->addChild(itemText);

	Menu* menu = Menu::create(item1, item2, item3, item4, item5, NULL);
	menu->setPosition(Vec2::ZERO);
	this->addChild(menu);

	auto information = LayerColor::create
	(Color4B(240, 240, 240, 255), 320, 50);
	this -> addChild(information);

	auto games_controller = Sprite::create("res/Bang/games_controller.png");
	games_controller->setPosition(Vec2(130, 25));
	information->addChild(games_controller);

	auto about = MenuItemImage::create(   "res/Bang/about.png", "res/Bang/aboutSel.png");
	auto leader = MenuItemImage::create(  "res/Bang/leader.png","res/Bang/leaderSel.png");
	auto info = MenuItemImage::create(	  "res/Bang/info.png",  "res/Bang/infoSel.png");
	auto ach = MenuItemImage::create(	  "res/Bang/ach.png",   "res/Bang/achSel.png");
	auto facebook = MenuItemImage::create("res/Bang/facebook.png", "res/Bang/facebookSel.png");
	

	auto bottomMenu = Menu::create(about, leader, ach, facebook, info, NULL);
	bottomMenu->alignItemsHorizontally();
	bottomMenu->alignItemsHorizontallyWithPadding(20);
	bottomMenu->setPosition(Vec2(320 / 2, 25));
	information->addChild(bottomMenu);


	auto cache = SpriteFrameCache::getInstance();
	cache->addSpriteFramesWithFile("res/Bang/menu_1.plist");

	auto animation = Animation::create();
	for (int i = 1; i <= 63; i++)
	{
		animation->addSpriteFrame(cache->getSpriteFrameByName(StringUtils::format("menu_1_%04d.png", i)));
	}
	animation->setDelayPerUnit(0.045f);
	auto animate = Animate::create(animation);

	auto aniSprite = Sprite::create();
	aniSprite->setPosition(Vec2(160, 480/3-60));
	aniSprite->runAction(RepeatForever::create(Sequence::create(animate, NULL)));
	this->addChild(aniSprite);
	
	


	return true;
}

void MenuScene::Func1(cocos2d::Ref* pSender)
{
	Director::getInstance()->replaceScene(GameStartScene::createScene());
}
