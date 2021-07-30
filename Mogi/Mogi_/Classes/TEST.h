#pragma once
#include "cocos2d.h"

USING_NS_CC;

class TEST : public cocos2d::LayerColor
{
public:
	CREATE_FUNC(TEST);

    static cocos2d::Scene* createScene();

	cocos2d::Sprite* sprite;
	cocos2d::Sprite* roulette[7];

    virtual bool init();
	void update(float delta);
	void Roulette();
    
    // a selector callback
    void menuCloseCallback(cocos2d::Ref* pSender);

	virtual void onTouchesBegan(const std::vector<Touch*>& touches, Event *unused_event);//´­·¶À»¶§
    
	bool R, T;
    int  rullcount = 0;

    // implement the "static create()" method manually
   
};