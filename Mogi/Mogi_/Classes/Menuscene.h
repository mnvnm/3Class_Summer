#pragma once
#include "cocos2d.h"

class MenuScene : public cocos2d::LayerColor
{
public:
	CREATE_FUNC(MenuScene);

    static cocos2d::Scene* createScene();

	cocos2d::Sprite* sprite1;

    virtual bool init();
	//void update(float delta);

	void Func1(cocos2d::Ref* pSender);
	void Func2(cocos2d::Ref* pSender);
    
    // a selector callback
    void menuCloseCallback(cocos2d::Ref* pSender);

    // implement the "static create()" method manually
   
};