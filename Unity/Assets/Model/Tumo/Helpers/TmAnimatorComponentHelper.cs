﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using UnityEngine;

namespace ETModel
{
    public static class TmAnimatorComponentHelper
    {
        #region
        public static void AnimSet(this TmAnimatorComponent self, float v)
        {
            self.AnimSet("Move", 0, v);
        }

        public static void AnimSet(this TmAnimatorComponent self, float h, float v)
        {
            self.AnimSet("Move", h, v);
        }

        public static void AnimSet(this TmAnimatorComponent self, string moveState)
        {
            self.AnimSet(moveState, 0, 0);
        }

        static void AnimSet(this TmAnimatorComponent self, string moveState, float h, float v)
        {
            switch (moveState)
            {
                case "Move":    //播放行走动画    
                    self.animator.SetBool("Move", true);
                    self.animator.SetFloat("Vblend", v);
                    self.animator.SetFloat("Hblend", h);
                    self.animator.SetBool("Attack", false);
                    //Debug.Log(" move：v" + v);
                    break;
                case "Walk":    //播放行走动画    
                    self.animator.SetBool("Move", true);
                    self.animator.SetFloat("Vblend", -1f);
                    self.animator.SetFloat("Hblend", 0);
                    self.animator.SetBool("Attack", false);
                    break;
                case "Run":     //播放追击动画  
                    self.animator.SetBool("Move", true);
                    self.animator.SetFloat("Vblend", 1f);
                    self.animator.SetFloat("Hblend", 0);
                    self.animator.SetBool("Attack", false);
                    break;
                case "Attack":  //播放攻击动画
                    self.animator.SetBool("Attack", true);
                    self.animator.SetBool("Move", false);
                    break;
                case "Idle":    //播放休息动画
                    self.animator.SetBool("Move", false);
                    self.animator.SetBool("Attack", false);
                    break;
                case "Die":     //播放死亡动画 
                    self.animator.SetTrigger("Die");
                    self.animator.SetBool("Attack", false);
                    self.animator.SetBool("Move", false);
                    break;
                case "Jump":    //播放跳跃动画 
                    self.animator.SetTrigger("Jump");
                    break;
                case "Hit":    //播放挨打动画 
                    self.animator.SetTrigger("Hit");
                    break;
                case "SkillOne":    //播放攻击特效1动画 
                    self.animator.SetTrigger("SkillOne");
                    break;
                case "SkillTwo":    //播放攻击特效1动画 
                    self.animator.SetTrigger("SkillTwo");
                    break;
                case "SkillThree":  //播放攻击特效1动画 
                    self.animator.SetTrigger("SkillThree");
                    break;
                case "SkillZero":  //播放基本攻击动画 
                    self.animator.SetTrigger("SkillZero");
                    break;
            }
        }
        #endregion

        #region
        public static void AnimSet(this AnimatorComponent self, float v)
        {
            self.AnimSet("Move", 0, v);
        }

        public static void AnimSet(this AnimatorComponent self, float h, float v)
        {
            self.AnimSet("Move", h, v);
        }

        public static void AnimSet(this AnimatorComponent self, string moveState)
        {
            self.AnimSet(moveState, 0, 0);
        }

        static void AnimSet(this AnimatorComponent self, string moveState, float h, float v)
        {
            switch (moveState)
            {
                case "Move":    //播放行走动画    
                    self.Animator.SetBool("Move", true);
                    self.Animator.SetFloat("Vblend", v);
                    self.Animator.SetFloat("Hblend", h);
                    self.Animator.SetBool("Attack", false);
                    //Debug.Log(" move：v" + v);
                    break;
                case "Walk":    //播放行走动画    
                    self.Animator.SetBool("Move", true);
                    self.Animator.SetFloat("Vblend", -1f);
                    self.Animator.SetFloat("Hblend", 0);
                    self.Animator.SetBool("Attack", false);
                    break;
                case "Run":     //播放追击动画  
                    self.Animator.SetBool("Move", true);
                    self.Animator.SetFloat("Vblend", 1f);
                    self.Animator.SetFloat("Hblend", 0);
                    self.Animator.SetBool("Attack", false);
                    break;
                case "Attack":  //播放攻击动画
                    self.Animator.SetBool("Attack", true);
                    self.Animator.SetBool("Move", false);
                    break;
                case "Idle":    //播放休息动画
                    self.Animator.SetBool("Move", false);
                    self.Animator.SetBool("Attack", false);
                    break;
                case "Die":     //播放死亡动画 
                    self.Animator.SetTrigger("Die");
                    self.Animator.SetBool("Attack", false);
                    self.Animator.SetBool("Move", false);
                    break;
                case "Jump":    //播放跳跃动画 
                    self.Animator.SetTrigger("Jump");
                    break;
                case "Hit":    //播放挨打动画 
                    self.Animator.SetTrigger("Hit");
                    break;
                case "SkillOne":    //播放攻击特效1动画 
                    self.Animator.SetTrigger("SkillOne");
                    break;
                case "SkillTwo":    //播放攻击特效1动画 
                    self.Animator.SetTrigger("SkillTwo");
                    break;
                case "SkillThree":  //播放攻击特效1动画 
                    self.Animator.SetTrigger("SkillThree");
                    break;
                case "SkillZero":  //播放基本攻击动画 
                    self.Animator.SetTrigger("SkillZero");
                    break;
            }
        }
        #endregion

    }
}
