#pragma once
#include "cocos2d.h"

USING_NS_CC;

using namespace std;

class StageC : public LayerColor
{
public:

	CREATE_FUNC(StageC);
	
    static Scene* createScene();

	void Stage1(Ref* pSender);
	void Stage2(Ref* pSender);

	Sprite* Main2;
	MogiMain* mogi;
	bool Stage_choose = false;
	int Stage_Next;
	int Play_sound;

	int MogiStage_Sound;
	float MogiStage_Sound_Scale = 0;


	MenuItemSprite* Stg;
	MenuItemImage* left_Next_button;
	MenuItemImage* right_Next_button;

	MenuItemSprite* Stg2;
	MenuItemImage* left_Next_button_2;
	MenuItemImage* right_Next_button_2;

    bool init();
	void update(float delta);
	void menuCloseCallback(Ref* pSender);
};