#pragma once
#include "cocos2d.h"

USING_NS_CC;

using namespace std;

class StageC_2 : public cocos2d::LayerColor
{
public:

	CREATE_FUNC(StageC_2);
	
    static cocos2d::Scene* createScene();

	bool Stage_choose = false;
	float MogiChoose2_Sound_Scale = 0;


	void Stage2(Ref* pSender);

	MenuItemSprite* Stg2;
	MenuItemImage* left_Next_button_2;
	MenuItemImage* right_Next_button_2;

    bool init();/*
	void update(float delta);*/
	void menuCloseCallback(Ref* pSender);
};