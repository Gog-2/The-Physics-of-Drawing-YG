using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class BallBox : MonoBehaviour
{
   private BoxSideOpen SideOpen;
   private int _id;
   [SerializeField]private BallId _ballPrefab;
   private BallId _ballCurrent;
   [SerializeField]private Transform _ballContainer;
   [SerializeField]private GameObject[] _sides;
   private TaskManager _taskManager;

   private void Start()
   {
       _taskManager = TaskManager.Instance;
       _taskManager.StartGameEvent += Activate;   
   }

   public void Intz(int amount, BoxSideOpen sideOpen,int id)
   {
       _id = id;
       SpawningBalls(amount);
       AlphaSide(sideOpen);
       SideOpen = sideOpen;
   }

   public void Activate()
   {
       switch (SideOpen)
       {
           case BoxSideOpen.Left:
               _sides[0].SetActive(false);
               break;
           case BoxSideOpen.Right:
               _sides[1].SetActive(false);
               break;
           case BoxSideOpen.Top:
               _sides[2].SetActive(false);
               break;
           case BoxSideOpen.Bottom:
               _sides[3].SetActive(false);
               break;
           case BoxSideOpen.Everything:
               foreach (GameObject sid in _sides) sid.SetActive(false);
               break;
       }
   }

   private void SpawningBalls(int ammount)
   {
       float size = 0.7f / ammount;
       _ballPrefab.gameObject.transform.localScale = new Vector3(size,size,size);
       StartCoroutine(SpawningBall(ammount));
   }

   private IEnumerator SpawningBall(int ammount)
   {
       for (int i = 0; i < ammount; i++)
       {
           _ballCurrent = Instantiate(_ballPrefab, _ballContainer.position, Quaternion.identity);
           _ballCurrent.transform.position = new Vector3(_ballCurrent.transform.position.x,_ballCurrent.transform.position.y,0);
           _ballCurrent.Id = _id;
           Rigidbody2D _ballRb = _ballCurrent.GetComponent<Rigidbody2D>();
           _ballRb.AddForce(new Vector2(-0.2f,-0.2f), ForceMode2D.Impulse);
           yield return new WaitForSeconds(0.5f);
       }  
   }

   private void AlphaSide(BoxSideOpen sideOpen)
   {
       switch (sideOpen)
       {
           case BoxSideOpen.Left:
               ChangeAlpha(0);
               break;
           case BoxSideOpen.Right:
               ChangeAlpha(1);
               break;
           case BoxSideOpen.Top:
               ChangeAlpha(2);
               break;
           case BoxSideOpen.Bottom:
               ChangeAlpha(3);
               break;
           case BoxSideOpen.Everything:
               for (int i = 0; i < _sides.Length; i++) ChangeAlpha(i);
               break;
       }
   }

   private void ChangeAlpha(int id)
   {
       SpriteRenderer sr;
       sr = _sides[id].GetComponent<SpriteRenderer>();
       sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f);
   }
}

public enum BoxSideOpen
{
    Top,
    Bottom,
    Left,
    Right,
    Everything
}