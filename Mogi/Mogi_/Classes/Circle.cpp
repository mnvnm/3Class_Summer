#include "Circle.h"

MosCircle::MosCircle()
{
	this->autorelease();
	this->init();
	this->scheduleUpdate();

	clip = ClippingNode::create(Sprite::create("mogi/stan.png"));
	clip->setAlphaThreshold(0.0f);
	clip->setInverted(false);
	this->addChild(clip, 5);
	
	Sprite* spr;
	viewBg = Sprite::create("mogi/CheckBar_Bg.png");
	viewBg->setRotation(20);
	clip->addChild(viewBg);

	spr = Sprite::create("mogi/CheckBar.png");
	this->addChild(spr , 0);

	spr = Sprite::create("mogi/CheckBar_1.png");
	this->addChild(spr, 10);

	viewBlue = Sprite::create("mogi/CheckBar_2.png");
	clip->addChild(viewBlue);

	debugDraw = DrawNode::create();
	this->addChild(debugDraw);	
}

void MosCircle::update(float _delata)
{
	float fRed = CC_DEGREES_TO_RADIANS(-22 + 90);
	float fBlue = CC_DEGREES_TO_RADIANS(-userAngle + 90);

	debugDraw->clear();
	//debugDraw->drawDot(Vec2::ZERO, 10, Color4F::RED);
	//debugDraw->drawLine(Vec2::ZERO, Vec2(0, 300), Color4F::BLUE);
	//debugDraw->drawLine(Vec2::ZERO, Vec2(cosf(fRed), sinf(fRed)) * 300, Color4F::YELLOW);
	//debugDraw->drawLine(Vec2::ZERO, Vec2(cosf(fBlue), sinf(fBlue)) * 300, Color4F::BLUE);
}

void MosCircle::setUserAngle(float _angle)
{
	userAngle = _angle;
	clip->setRotation(_angle - 90);
	viewBlue->setRotation(90 - _angle);
	viewBg->setRotation(90 - _angle);
}

void MosCircle::setUserRotation(float _rot)
{
	userRotation = _rot;
	this->setRotation(userRotation);
}

int MosCircle::isMatch(float _rot)
{
	float st = userRotation;
	float ed = st + 20;
	float rd = st + userAngle;
	ed = ((int)ed % 360) + (ed - (int)ed);
	rd = ((int)rd % 360) + (rd - (int)rd);
	CCLOG("%f, %f : %f : %f", _rot, st, ed, rd);

	// 레드존
	if (st > ed)
	{
		if (st < _rot) return 1;
		else if (ed > _rot) return 1;
	}
	else if (st <= ed)
	{
		if (st < _rot && _rot < ed) return 1;
	}

	// 블루존
	if (ed > rd)
	{
		if (ed < _rot) return 2;
		else if (rd > _rot) return 2;
	}
	else if (ed <= rd)
	{
		if (ed < _rot && _rot < rd) return 2;
	}

	return 0;
}
