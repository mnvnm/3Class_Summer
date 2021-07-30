#pragma once
#include "cocos2d.h"

#pragma execution_character_set("utf-8")

USING_NS_CC;
using namespace std;

class MosCircle : public Sprite
{
	ClippingNode* clip;
	Sprite* viewBlue;
	Sprite* viewBg;
	DrawNode* debugDraw;

public:
	MosCircle();
	~MosCircle() {}

	void update(float _delata);

	float userAngle = 90;
	float userRotation = 0;
	void setUserAngle(float _angle);
	void setUserRotation(float _rot);
	int isMatch(float _rot);
};