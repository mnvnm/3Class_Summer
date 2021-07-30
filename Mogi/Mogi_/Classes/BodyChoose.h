#pragma once
#include "cocos2d.h"
#include <iostream>

USING_NS_CC;

using namespace std;

class MogiM : public cocos2d::LayerColor
{													
public:
	CREATE_FUNC(MogiM);
	Vec2 pos;

	Sprite* Baby;
	Sprite* Head;
	Sprite* Arm;
	Sprite* Leg;
	Sprite* Check;
	Sprite* Check_X[3];

	Sprite* GameClear_bg;

	MenuItemSprite* GameClear_main;
	MenuItemSprite* GameClear_next;

	bool GameClear = false;

	int CheckCase = 1;

	bool CheckBool = false;
	bool replace = false;

	int Clear_sound;

    static cocos2d::Scene* createScene();

    bool init();
	void update(float delta);

	void Main_menu(Ref* pSender);
	void Main_Next(Ref* pSender);
													
	virtual void onKeyPressed(EventKeyboard::KeyCode keyCode, Event* event);
	virtual void onKeyReleased(EventKeyboard::KeyCode keyCode, Event* event);

	void onTouchesBegan(const std::vector<Touch*>& touches, Event *unused_event);
	//void onMouseMove(cocos2d::Event* event);
	
	int f_body[3];
};