// using HellTap.PoolKit;
// using System.Collections.Generic;
// using UnityEngine;
//
// namespace Game
// {
//     public class PoolManager : MonoBehaviour
//     {
//         static Pool pool;
//         static Transform selfTransform;
//
//         private void Start()
//         {
//             pool = PoolKit.GetPool("Pooling");
//             selfTransform = transform;
//         }
//
//         public static void Spawn(Transform trans, Vector3 pos = default(Vector3), Quaternion quaternion = default(Quaternion), Transform parent = null)
//         {
//             Transform temp = pool.Spawn(trans, pos, quaternion, parent == null ? selfTransform : parent);
//       
//         }
//         public static T Spawn<T>(Transform trans, Transform parent)
//         {
//             T obj = pool.Spawn(trans, Vector3.zero, Quaternion.identity, parent).GetComponent<T>();
//             return obj;
//         }
//         public static T Spawn<T>(Transform trans, Vector3 pos = default(Vector3), Quaternion quaternion = default(Quaternion), Transform parent = null)
//         {
//             Transform temp = pool.Spawn(trans, pos, quaternion, parent == null ? selfTransform : parent);
//            
//             return temp.GetComponent<T>();
//         }
//
//         public static T Spawn<T>(string name, Vector3 pos = default(Vector3), Quaternion quaternion = default(Quaternion), Transform parent = null)
//         {
//             Transform temp = pool.Spawn(name, pos, quaternion, parent == null ? selfTransform : parent);
//
//             return temp.GetComponent<T>();
//         }
//
//         public static T Spawn<T>(GameObject g, Vector3 pos = default(Vector3), Quaternion quaternion = default(Quaternion), Transform parent = null)
//         {
//             Transform temp = pool.Spawn(g, pos, quaternion, parent == null ? selfTransform : parent);
//             return temp.GetComponent<T>();
//         }
//         public static void Despawn(Transform trans)
//         {
//             pool.Despawn(trans);
//         }
//         public static void Despawn(GameObject g)
//         {
//             pool.Despawn(g);
//         }
//         public static List<T> GetActiveInstances<T>()
//         {
//             List<T> list = new List<T>();
//             var temps = pool.GetActiveInstances();
//             for (int i = 0; i < temps.Length; i++)
//             {
//                 if (temps[i].TryGetComponent<T>(out T item))
//                 {
//                     list.Add(item);
//                 }
//             }
//             return list;
//         }
//     }
// }