using RPGM.Core;
using RPGM.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RPGM.Gameplay
{
    /// <summary>
    /// Main class for implementing NPC game objects.
    /// </summary>
    public class NPCController : MonoBehaviour
    {
        public ConversationScript[] conversations;
        public bool playerAlreadyTalk;
        public bool triggerEndScene;
        public bool hint;
        

        Quest activeQuest = null;

        Quest[] quests;

        GameModel model = Schedule.GetModel<GameModel>();

        void OnEnable()
        {
            quests = gameObject.GetComponentsInChildren<Quest>();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var c = GetConversation();
            if (c != null)
            {
                var ev = Schedule.Add<Events.ShowConversation>();
                ev.conversation = c;
                ev.npc = this;
                ev.gameObject = gameObject;
                ev.conversationItemKey = "";
            }
            if (playerAlreadyTalk == false)
            {
                Score.instance.AddPoint();
            }
            playerAlreadyTalk = true;
            if (hint == true)
            {
                Score.instance.SpecialQuest();
            }
            hint = false;
        }
        


        public void CompleteQuest(Quest q)
        {
            if (activeQuest != q) throw new System.Exception("Completed quest is not the active quest.");
            foreach (var i in activeQuest.requiredItems)
            {
                model.RemoveInventoryItem(i.item, i.count);
            }
            activeQuest.RewardItemsToPlayer();
            activeQuest.OnFinishQuest();
            activeQuest = null;
            
            if (triggerEndScene == true)
            {
                SceneManager.LoadScene("INTP");
                if (Score.score < 5 && Score.finishedquest < 3 && Score.specialquest == 0)
                {
                    SceneManager.LoadScene("INTP");
                }
                else if (Score.score < 5 && Score.finishedquest > 2 && Score.specialquest == 0)
                {
                    SceneManager.LoadScene("INFP");
                }
                else if (Score.score < 5 && Score.finishedquest > 2 && Score.specialquest == 1)
                {
                    SceneManager.LoadScene("ISFP");
                }
                else if (Score.score < 5 && Score.finishedquest < 3 && Score.specialquest == 1)
                {
                    SceneManager.LoadScene("ISTP");
                }
                else if (Score.score > 4 && Score.finishedquest < 3 && Score.specialquest == 0)
                {
                    SceneManager.LoadScene("ENTP");
                }
                else if (Score.score > 4 && Score.finishedquest >2 && Score.specialquest == 0)
                {
                    SceneManager.LoadScene("ENFP");
                }
                else if (Score.score > 4 && Score.finishedquest > 2 && Score.specialquest == 1)
                {
                    SceneManager.LoadScene("ESFJ");
                }
                else if (Score.score > 4 && Score.finishedquest <3 && Score.specialquest == 1)
                {
                    SceneManager.LoadScene("ESTP");
                }
                else
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }

        public void StartQuest(Quest q)
        {
            if (activeQuest != null) throw new System.Exception("Only one quest should be active.");
            activeQuest = q;
        }

        ConversationScript GetConversation()
        {
            if (activeQuest == null)
                return conversations[0];
            foreach (var q in quests)
            {
                if (q == activeQuest)
                {
                    if (q.IsQuestComplete())
                    {
                        CompleteQuest(q);
                        return q.questCompletedConversation;
                    }
                    return q.questInProgressConversation;
                }
            }
            return null;
        }
    }
}