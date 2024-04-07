using UnityEngine;

public class Teleportation : MonoBehaviour
{
   [SerializeField] private string enterPointName;
   [SerializeField] private string exitPointName;
   [SerializeField] private bool isEnter;
   private Transform _destination;
   
    
   public float distance = 0.2f;

   private void Start()
   {
      if (isEnter == false)
      {
         _destination = GameObject.FindGameObjectWithTag(enterPointName).GetComponent<Transform>();
      }
      else
      {
         _destination = GameObject.FindGameObjectWithTag(exitPointName).GetComponent<Transform>();
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         if (Vector2.Distance(transform.position,other.transform.position)> distance)
         {
            var destinationPosition = _destination.position;
            other.transform.position = new Vector2(destinationPosition.x, destinationPosition.y);
         }
      }
   }
}
