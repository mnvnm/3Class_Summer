#include "HelloWorldScene.h"
#include "TEST.h"
#include "BangScene.h"
#include "MenuScene.h"'
#include "SimpleAudioEngine.h"

#pragma execution_character_set("utf-8")

USING_NS_CC;

Scene* TEST::createScene()
{
	auto scene = Scene::create();
	auto layer = TEST::create();

	scene->addChild(layer);

	return scene;
}

static void problemLoading(const char* filename)
{
    printf("Error while loading: %s\n", filename);
    printf("Depending on how you compiled you might have to add 'Resources/' in front of filenames in HelloWorldScene.cpp\n");
}

bool TEST::init()
{
    if ( !LayerColor::initWithColor(Color4B(255,255,255,255)) )
    {
        return false;
    }

	this->scheduleUpdate();
	this->setTouchEnabled(true);

    auto visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

    auto closeItem = MenuItemImage::create("CloseNormal.png","CloseSelected.png",CC_CALLBACK_1(TEST::menuCloseCallback, this));

    if (closeItem == nullptr ||
        closeItem->getContentSize().width <= 0 ||
        closeItem->getContentSize().height <= 0)
    {
        problemLoading("'CloseNormal.png' and 'CloseSelected.png'");
    }
    else
    {
        float x = origin.x + visibleSize.width - closeItem->getContentSize().width/2;
        float y = origin.y + closeItem->getContentSize().height/2;
        closeItem->setPosition(Vec2(x,y));
    }
    auto menu = Menu::create(closeItem, NULL);
    menu->setPosition(Vec2::ZERO);
    this->addChild(menu, 1);

    auto label = Label::create("·Î°í!", "arial", 24);
    label->setPosition(Vec2(origin.x + visibleSize.width/2,
                       origin.y + visibleSize.height - label->getContentSize().height));
    this->addChild(label, 1);

	sprite = Sprite::create();
    sprite->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));

	//sprite->setVisible(false);

    this->addChild(sprite, 0);

	roulette[0] = Sprite::create("res/Bang/fps_images.png", Rect(16 * (2 + 0), 0, 16, 32));
	roulette[1] = Sprite::create("res/Bang/fps_images.png", Rect(16 * (2 + 1), 0, 16, 32));
	roulette[2] = Sprite::create("res/Bang/fps_images.png", Rect(16 * (2 + 2), 0, 16, 32));
	roulette[3] = Sprite::create("res/Bang/fps_images.png", Rect(16 * (2 + 3), 0, 16, 32));
	roulette[4] = Sprite::create("res/Bang/fps_images.png", Rect(16 * (2 + 4), 0, 16, 32));
	roulette[4] = Sprite::create("res/Bang/fps_images.png", Rect(16 * (2 + 4), 0, 16, 32));
	roulette[5] = Sprite::create("res/Bang/fps_images.png", Rect(16 * (2 + 5), 0, 16, 32));
	roulette[6] = Sprite::create("res/Bang/fps_images.png", Rect(16 * (2 + 6), 0, 16, 32));
	roulette[7] = Sprite::create("res/Bang/fps_images.png", Rect(16 * (2 + 7), 0, 16, 32));

	roulette[0]->setPosition(Vec2(160, 240));
	roulette[1]->setPosition(Vec2(160, 240));
	roulette[2]->setPosition(Vec2(160, 240));
	roulette[3]->setPosition(Vec2(160, 240));
	roulette[4]->setPosition(Vec2(160, 240)); 
	roulette[5]->setPosition(Vec2(160, 240));
	roulette[6]->setPosition(Vec2(160, 240));
	roulette[7]->setPosition(Vec2(160, 240));


    return true;
}

void TEST::Roulette()
{
	for (int i = 0; i < 7;i++)
	{
		this->addChild(roulette[i]);
	}
}


void TEST::menuCloseCallback(Ref* pSender)
{
	Director::getInstance()->replaceScene(MenuScene::createScene());
}

 void TEST::onTouchesBegan(const std::vector<Touch*>& touches, Event *unused_event)
{
	 if (rullcount == 0)
	 {
		 Roulette();
		//CallFunc::create(CC_CALLBACK_0(TEST::Roulette, this));
		rullcount++;
	 }
}

void TEST::update(float delta)
{
	

	
}
