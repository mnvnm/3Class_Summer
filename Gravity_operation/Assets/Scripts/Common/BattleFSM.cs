using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFSM 
{
    public delegate void DelegateFunc();

    public class CState
    {
        public DelegateFunc m_OnEnterFunc = null;
        public DelegateFunc m_OnExitFunc = null;
        public virtual void Initialize(DelegateFunc onEnterfunc) {
            m_OnEnterFunc = new DelegateFunc(onEnterfunc);
        }
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }

    public class CEditState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate() { }
        public override void OnExit() { }
    }
    public class CWaveState : CState
    {
        public override void OnEnter(){
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate()   {     }
        public override void OnExit()     {     }

    }
    public class CGameState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate() { }
        public override void OnExit() {
            if (m_OnExitFunc != null)
                m_OnExitFunc();
        }

    }
    public class CResultState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate() { }
        public override void OnExit() { }
    }
    public class CActionState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate() { }
        public override void OnExit() { }
    }


    private CState m_curState = null;
    private CState m_newState = null;

    private CState m_kEdit = new CEditState();
    private CState m_kWave = new CWaveState();
    private CState m_kGame = new CGameState();
    private CState m_kResult = new CResultState();
    private CState m_kAct = new CActionState();


    public void Initialize(DelegateFunc kEdit, DelegateFunc kWave, DelegateFunc kGame, DelegateFunc kResult, DelegateFunc kAct)
    {
        m_kEdit.Initialize(kEdit);
        m_kWave.Initialize(kWave);
        m_kGame.Initialize(kGame);
        m_kResult.Initialize(kResult);
        m_kAct.Initialize(kAct);
    }

    // 상태 변화 셋팅
    public void SetState( CState kState )
    {
        m_newState = kState;
    }

    // 
    public void OnUpdate()
    {
        if( m_newState != null )
        {
            if (m_curState != null)
                m_curState.OnExit();

            m_curState = m_newState;
            m_newState = null;
            m_curState.OnEnter();
        }

        if (m_curState != null)
            m_curState.OnUpdate();
    }


    public void SetEditState()    {
        SetState(m_kEdit);
    }
    public void SetWaveState()    {
        SetState(m_kWave);
    }
    public void SetGameState()    {
        SetState(m_kGame);
    }
    public void SetResultState()   {
        SetState(m_kResult);
    }
    public void SetActState()
    {
        SetState(m_kAct);
    }
    public void SetCallback_GameStateOnExit( DelegateFunc func)
    {
        m_kGame.m_OnExitFunc = new DelegateFunc(func);
    }
    public bool IsCurState( CState kState )
    {
        if (m_curState == null) 
            return false;

        return (m_curState == kState) ? true : false;
    }
    public CState GetCurState()
    {
        return m_curState;
    }

    public void SetNoneState()
    {
        m_newState = null;
        m_curState = null;
    }

    public bool IsResultState()
    {
        return (m_curState == m_kResult);
    }

    public bool IsGameState()
    {
        return (m_curState == m_kGame);
    }
    public bool IsEditState()
    {
        return (m_curState == m_kEdit);
    }

    public bool IsActState()
    {
        return (m_curState == m_kAct);
    }
    public bool IsNoneState()
    {
        return (m_curState == null);
    }

}
