#pragma once
#include "cocos2d.h"

USING_NS_CC;

using namespace std;

class MogiMain : public cocos2d::LayerColor
{
public:
	CREATE_FUNC(MogiMain);
	
	Sprite* Main;
	Sprite* Main2;

	int button;
	float MogiMain_Sound_Scale = 0;

	int MogiMain_bg;
	bool Stage_choose = false;

	double Mt;
	
    static cocos2d::Scene* createScene();

    bool init();
	void update(float delta);
	void menuCloseCallback(Ref* pSender);
};