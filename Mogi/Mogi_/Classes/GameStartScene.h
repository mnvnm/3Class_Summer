#pragma once
#include "cocos2d.h"

USING_NS_CC;

class GameStartScene : public cocos2d::LayerColor
{
public:
	CREATE_FUNC(GameStartScene);

	bool BullCount = true;
	bool NPCbang = true;

    static cocos2d::Scene* createScene();

	cocos2d::Animation* Stand;
	cocos2d::Animation* Walk;
	cocos2d::Animation* Coward;
	cocos2d::Animation* Gunsling;
	cocos2d::Animation* Shoot;
	cocos2d::Animation* Miss;
	cocos2d::Animation* Death;

	cocos2d::Animate* ani_walk;
	cocos2d::Animate* ani_Bang;
	cocos2d::Animate* ani_Coward;
	cocos2d::Animate* ani_Gunsling;
	cocos2d::Animate* ani_Miss;
	cocos2d::Animate* ani_Death;

	cocos2d::Sprite* spr_temp;
	cocos2d::Sprite* spr_temp1;
	cocos2d::Sprite* spr_temp2;
	cocos2d::Sprite* spr_temp3;
	cocos2d::Sprite* spr_temp4;
	cocos2d::Sprite* death_temp;

	cocos2d::Sprite* SpriteCom;
	cocos2d::Sprite* SpritePlayer;

	cocos2d::Sprite* game;

	cocos2d::Layer* GameInfo;

    virtual bool init();
	//void update(float delta);

	void Home(cocos2d::Ref* pSender);
	void Start(cocos2d::Ref* pSender);

	void Ready();
	void Steady();
	void Bang();

	void update(float delta);

	virtual void onTouchesBegan(const std::vector<Touch*>& touches, Event *unused_event);//눌렀을때
	virtual void onTouchesMoved(const std::vector<Touch*>& touches, Event *unused_event);//드래그 등 움직이는걸 했을때
	virtual void onTouchesEnded(const std::vector<Touch*>& touches, Event *unused_event);//누르고 떼었을때
	virtual void onTouchesCancelled(const std::vector<Touch*>&touches, Event *unused_event);//홈 화면 등을 눌러 게임을 나갔을때

    //implement the "static create()" method manually
	
};