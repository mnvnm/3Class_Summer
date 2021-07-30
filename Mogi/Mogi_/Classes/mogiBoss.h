#pragma once
#include "cocos2d.h"

USING_NS_CC;

using namespace std;

class MogiMove : public cocos2d::LayerColor
{
public:
	CREATE_FUNC(MogiMove);
	Vec2 pos;

	Sprite* SpritePlayer;
	Sprite* Body_c;
	Sprite* body;

	Sprite* SpriteBoss;
	Sprite* Boss_c;
	Sprite* Boss;
	//패턴1//
	Sprite* Pt1_1;
	Sprite* Pt1_2;
	Sprite* Pt1_danger_1;
	Sprite* Pt1_danger_2;
	PhysicsBody* PT1_phy;
	PhysicsBody* PT1_1_phy;
	bool use_pt1;
	//패턴2//
	Sprite* Pt2_1;
	Sprite* Pt2_2;
	Sprite* Pt2_danger_1;
	Sprite* Pt2_danger_2;
	PhysicsBody* PT2_phy;
	PhysicsBody* PT2_1_phy;
	bool use_pt2;
	//패턴3//
	Sprite* Pt3_1;
	Sprite* Pt3_2;
	Sprite* Pt3_danger_1;
	Sprite* Pt3_danger_2;
	PhysicsBody* PT3_phy;
	PhysicsBody* PT3_1_phy;
	bool use_pt3;
	//패턴4//
	Sprite* Pt4_1;
	Sprite* Pt4_2;
	Sprite* Pt4_danger_1;
	Sprite* Pt4_danger_2;
	PhysicsBody* PT4_phy;
	PhysicsBody* PT4_1_phy;
	bool use_pt4; 
	bool use_pt5; 

	bool Pattern_rand_b = false;
	int Pattern_random = 0;
	//
	Sprite* bg1;
	Sprite* bg2;

	Sprite* Gameover_bg;

	MenuItemSprite* Gameover_main;
	MenuItemSprite* Gameover_restart;

	bool Gameover = false;
	float Gameover_rotate = 0;
	int Hit_sound;

	int Hit_sound_1;
	int Hit_sound_2;
	int Hit_sound_3;

	int Dash_sound;

	void Main_menu(Ref* pSender);
	void Main_restart(Ref* pSender);
	//Label* l_score;

	bool left;
	bool right;
	bool up;
	bool down;

	int speed;

	bool dash;
	bool god;
	double dashCool;

	float end_time = 48;
	Label* Time;

	int pt4_angle;

	bool Ps;

	int MogiMove_bg;
	float Mogimove_Sound_Scale = 0;

    static cocos2d::Scene* createScene();

	/*PhysicsBody* H_phy;*/
	PhysicsBody* physics;

	Animate*  mogi_mate;
	Animation* mogi_Ani;

	/*Animate*  hand_mate;
	Animation* hand_ani;*/

    bool init();
	void update(float delta);

	Shake* shake;

	void Pt1();
	void Pt2();
	void Pt3();
	void Pt4();
	
	virtual void onKeyPressed(EventKeyboard::KeyCode keyCode, Event* event);
	virtual void onKeyReleased(EventKeyboard::KeyCode keyCode, Event* event);

	bool onContactBegin(PhysicsContact & contact);

	enum class PhysicsCategory
	{
		None = 0,
		Hero = 1,
		hand = 2,
		ALL = Hero | hand
	};
};