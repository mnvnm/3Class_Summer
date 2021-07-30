#pragma once
#include "cocos2d.h"
#include "Shake.h"
#include "Circle.h"

USING_NS_CC;

using namespace std;

class Mogi : public cocos2d::LayerColor
{
public:

	CREATE_FUNC(Mogi);
	Vec2 pos;

	Sprite* body;
	Sprite* wing;
	Sprite* SpritePlayer;
	Sprite* Body_c;
	Sprite* bg1;
	Sprite* bg2;


	Shake* shake;

	PhysicsBody* physics;

	Animate* mogi_mate;
	Animation* mogi_Ani;

	bool updown;
	float Move = 0;

	float PsT;
	bool Ps;
	bool start;

	int Efv;

	int Pco = 1;

    static cocos2d::Scene* createScene();

    bool init();
	void update(float delta);

	Sprite* gumijul[5];
	Sprite* gumi[5];
	Sprite* fish[5];
	Sprite* s_fish;
	Sprite* fish_c[5];
	Sprite* button;
	Sprite* button2;
	Sprite* Water;
	Sprite* S_Water;
	Sprite* Water_c;
	Sprite* suro[2];
	Sprite* Eat[10];

	Sprite* three;
	Sprite* two;
	Sprite* one;
	Sprite* Start_sp;

	Animate* fish_mate;
	Animation* fish_Ani;
	Animate* water_mate;
	Animation* water_Ani;

	PhysicsBody* G_Phy;
	PhysicsBody* F_Phy;
	PhysicsBody* B_Phy;
	PhysicsBody* W_Phy;
	PhysicsBody* S_Phy;
	PhysicsBody* Eat_Phy;

	Sprite* Gameover_bg;

	MenuItemSprite* Gameover_main;
	MenuItemSprite* Gameover_restart;

	Label* Food_L;

	int bgm;

	void M_1_Init();
	int WaterOpen = 0;
	int food;
	float FoodTime = 0;

	void pt1();
	bool PT1;
	void pt2();
	bool PT2;
	void pt3();
	bool PT3;

	bool sound = false;
	bool Gameover = false;
	bool GameStart = false;
	float Gameover_rotate = 0;

	int mogi_Hit_sound;
	int mogi_Hit_sound_1;
	int mogi_Hit_sound_2;
	int mogi_Hit_sound_3;

	int mosquito = 0;//뒤에 true가 붙으면 무한재생
	int Mogi_bg = 0;//뒤에 true가 붙으면 무한재생
	double Sound_Scale = 0;

	void Main_menu(Ref* pSender);
	void Main_restart(Ref* pSender);

	virtual void onKeyPressed(EventKeyboard::KeyCode keyCode, Event* event);
	virtual void onKeyReleased(EventKeyboard::KeyCode keyCode, Event* event);

	bool onContactBegin(PhysicsContact & contact);

	void onMouseMove(cocos2d::Event* event);

	enum class PhysicsCategory
	{
		None = 0,
		Hero = 1,
		Mon_r = 2,
		Spider = 4,
		eye = 8,
		Button = 16,
		Eat = 32,
		ALL = Hero | Spider | eye
	};
};