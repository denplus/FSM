using TMPro;
using UnityEngine;
using ViewPresentation.Presenter;

namespace ViewPresentation.View
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;

        private void Start()
        {
            MoneyPresenter presenter = new MoneyPresenter(this);
        }

        public void SetMoneyCount(int currentMoneyCount)
        {
            moneyText.text = currentMoneyCount.ToString();
        }
    }
}