using UnityEngine;
using UnityEngine.UI;

namespace DroneInvader.Scripts
{
    public class PlayerHUDManager : MonoBehaviour
    {
        public static PlayerHUDManager Instance;
        public Image healthUI, healthMotion;

        public RectTransform logParent;
        public KillInfo logPrefab;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    
        private void Update()
        {
            healthMotion.fillAmount = healthMotion.fillAmount - healthUI.fillAmount > 0.01f ?
                Mathf.Lerp(healthMotion.fillAmount, healthUI.fillAmount, Time.deltaTime * 3f) : healthUI.fillAmount;
        }

        public void SetHealth(float ratio)
        {
            healthUI.fillAmount = ratio;
        }
        
        public void ShowKillLog(Entity killer, Entity victim)
        {
            KillInfo info = Instantiate(logPrefab, logParent);
            info.SetInfo(killer, victim);
            
            Destroy(info.gameObject, 5f);
        }

    }
}
