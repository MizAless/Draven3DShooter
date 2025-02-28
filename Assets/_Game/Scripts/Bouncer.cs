// using UnityEngine;
//
// namespace _Game.Scripts
// {
//     
//     
//     public class Bouncer : MonoBehaviour
//     {
//         private void BounceToPoint()
//         {
//             StartRotate(false);
//
//             GetComponent<Collider>().enabled = false;
//             _rigidbody.velocity = Vector3.zero;
//             _rigidbody.isKinematic = true;
//
//             var endPoint = GetRandomPoint();
//             var startPosition = transform.position;
//
//             Vector3[] path = new Vector3[]
//             {
//                 startPosition,
//                 startPosition + (endPoint - startPosition) * 0.5f + Vector3.up * 3f,
//                 endPoint
//             };
//
//             transform.DOPath(path, 1f, PathType.CatmullRom)
//                 .SetEase(Ease.OutCubic)
//                 .OnComplete(() => { Debug.Log("Предмет достиг цели!"); });
//         }
//
//         private Vector3 GetRandomPoint()
//         {
//             var randomVector = Random.insideUnitCircle;
//             var direction = new Vector3(randomVector.x, 0, randomVector.y) * 5f;
//
//             return _thrower.transform.position + direction;
//         }
//     }
// }