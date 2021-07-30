#pragma once
#include "cocos2d.h"
#include "Circle.h"

USING_NS_CC;

using namespace std;

class Blood : public cocos2d::LayerColor
{													
public:
	CREATE_FUNC(Blood);

	Sprite* Bg1;
	Sprite* Bg2;
	Sprite* check;
	Sprite* CheckBar_Bg;
	Sprite* CheckBar;
	Sprite* angryBar_Bg;
	Sprite* angryBar;
	Sprite* bloodBar_Bg;
	Sprite* bloodBar;

	float blood_fill_temp;
	float angry_fill_temp;

	float blood_fill_temp_2;
	float angry_fill_temp_2;

	int f_body[3];

	int fail;
	int success;
	int success_2;

    static cocos2d::Scene* createScene();

	float speed;
	float Rotate_speed;
	bool Check;
    bool init();
	void update(float delta);
													
	virtual void onKeyPressed(EventKeyboard::KeyCode keyCode, Event* event);
	virtual void onKeyReleased(EventKeyboard::KeyCode keyCode, Event* event);

	float angry = 0;
	float temp_1;
	float blood = 0;
	float temp_2;
	int Check_Number;

	bool mogi_b = true;
	bool replace = false;

	void onMouseMove(cocos2d::EventMouse* event);

	MosCircle* d;
};