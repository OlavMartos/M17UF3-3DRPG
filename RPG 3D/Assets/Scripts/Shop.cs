using UnityEngine;

public class Shop : MonoBehaviour
{
    public bool keyShopped;
    public bool firstExit;
    public MeshCollider ancientKeyCollider;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            CanvasManager.Instance.Shop();
            ShopFunction();
        }
    }

    private void ShopFunction()
    {
        if (keyShopped)
        {
            CanvasManager.Instance.shopCorrect.SetActive(true);
            CanvasManager.Instance.welcome.SetActive(false);
            return;
        }

        if (InputManager.Instance.IsInteracting() > 0.0f && GameManager.Instance.BrainCount < 10)
        {
            if(keyShopped == false) CanvasManager.Instance.shopIncorrect.SetActive(true);
        }

        if (InputManager.Instance.IsInteracting() > 0.0f && GameManager.Instance.BrainCount >= 10)
        {
            CanvasManager.Instance.shopCorrect.SetActive(true);
            keyShopped = true;
            ancientKeyCollider.isTrigger = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        CanvasManager.Instance.welcome.SetActive(false);
        CanvasManager.Instance.shopCorrect.SetActive(false);
        CanvasManager.Instance.shopIncorrect.SetActive(false);
        if(keyShopped && firstExit == false)
        {
            firstExit = true;
            GameManager.Instance.BrainCount -= 10;
            GameManager.Instance.BrainCountHUD(GameManager.Instance.BrainCount.ToString());
        }
    }
}
