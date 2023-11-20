// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using GameUtils;
//
// namespace EGamePlay
// {
//     [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
//     sealed class EnableUpdateAttribute : Attribute
//     {
//         public EnableUpdateAttribute()
//         {
//         }
//     }
//
//     public abstract partial class Entity
//     {
//         public static MasterEntity Master => MasterEntity.Instance;
//         public static bool EnableLog { get; set; } = false;
//
//         public static Entity NewEntity(Type entityType, long id = 0)
//         {
//             var entity = Activator.CreateInstance(entityType) as Entity;
//             entity.InstanceId = IdFactory.NewInstanceId();
//             if (id == 0) entity.Id = entity.InstanceId;
//             else entity.Id = id;
//             if (!Master.Entities.ContainsKey(entityType))
//             {
//                 Master.Entities.Add(entityType, new List<Entity>());
//             }
//             Master.Entities[entityType].Add(entity);
//             return entity;
//         }
//
//         public static T Create<T>() where T : Entity
//         {
//             return Create(typeof(T)) as T;
//         }
//
//         public static T Create<T>(object initData) where T : Entity
//         {
//             return Create(typeof(T), initData) as T;
//         }
//
//         private static void SetupEntity(Entity entity, Entity parent)
//         {
//             //var preParent = entity.Parent;
//             parent.SetChild(entity);
//             //if (preParent == null)
//             {
//                 entity.Awake();
//             }
//             entity.Start();
//         }
//
//         private static void SetupEntity(Entity entity, Entity parent, object initData)
//         {
//             //var preParent = entity.Parent;
//             parent.SetChild(entity);
//             //if (preParent == null)
//             {
//                 entity.Awake(initData);
//             }
//             entity.Start(initData);
//         }
//
//         public static Entity Create(Type entityType)
//         {
//             var entity = NewEntity(entityType);
//             if (EnableLog) Log.Debug($"Create {entityType.Name}={entity.Id}");
//             SetupEntity(entity, Master);
//             return entity;
//         }
//
//         public static Entity Create(Type entityType, object initData)
//         {
//             var entity = NewEntity(entityType);
//             if (EnableLog) Log.Debug($"Create {entityType.Name}={entity.Id}, {initData}");
//             SetupEntity(entity, Master, initData);
//             return entity;
//         }
//
//         public static void Destroy(Entity entity)
//         {
//             try
//             {
//                 entity.OnDestroy();
//             }
//             catch (Exception e)
//             {
//                 Log.Error(e);
//             }
//             entity.Dispose();
//         }
//     }
// }
