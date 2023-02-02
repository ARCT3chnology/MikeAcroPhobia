using UnityEngine;
using UnityEngine.UI;

namespace HelperClasses
{
    public class Helper_menuSelect : MonoBehaviour
    {
        public menuName selectName;
        Button btn;
        public bool ShouldChange = true;


        private void Start()
        {
            if (ShouldChange)
            {
                btn = this.GetComponent<Button>();
                btn.onClick.AddListener(OpenSelectedMenu_OnClick);
               
            }
        }
        public void OpenSelectedMenu_OnClick()
        {
                MenuManager.Instance.OpenMenu(selectName);
                
        }       


    }
}