using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework.Interface
{
    public interface IDbSet<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IQueryable, IEnumerable where TEntity : class
    {
        //
        // 摘要:
        //     Gets an System.Collections.ObjectModel.ObservableCollection`1 that represents
        //     a local view of all Added, Unchanged, and Modified entities in this set. This
        //     local view will stay in sync as entities are added or removed from the context.
        //     Likewise, entities added to or removed from the local view will automatically
        //     be added to or removed from the context.
        //
        // 备注:
        //     This property can be used for data binding by populating the set with data, for
        //     example by using the Load extension method, and then binding to the local data
        //     through this property. For WPF bind to this property directly. For Windows Forms
        //     bind to the result of calling ToBindingList on this property
        ObservableCollection<TEntity> Local { get; }

        //
        // 摘要:
        //     Adds the given entity to the context underlying the set in the Added state such
        //     that it will be inserted into the database when SaveChanges is called.
        //
        // 参数:
        //   entity:
        //     The entity to add.
        //
        // 返回结果:
        //     The entity.
        //
        // 备注:
        //     Note that entities that are already in the context in some other state will have
        //     their state set to Added. Add is a no-op if the entity is already in the context
        //     in the Added state.
        TEntity Add(TEntity entity);
        //
        // 摘要:
        //     Attaches the given entity to the context underlying the set. That is, the entity
        //     is placed into the context in the Unchanged state, just as if it had been read
        //     from the database.
        //
        // 参数:
        //   entity:
        //     The entity to attach.
        //
        // 返回结果:
        //     The entity.
        //
        // 备注:
        //     Attach is used to repopulate a context with an entity that is known to already
        //     exist in the database. SaveChanges will therefore not attempt to insert an attached
        //     entity into the database because it is assumed to already be there. Note that
        //     entities that are already in the context in some other state will have their
        //     state set to Unchanged. Attach is a no-op if the entity is already in the context
        //     in the Unchanged state.
        TEntity Attach(TEntity entity);
        //
        // 摘要:
        //     Creates a new instance of an entity for the type of this set. Note that this
        //     instance is NOT added or attached to the set. The instance returned will be a
        //     proxy if the underlying context is configured to create proxies and the entity
        //     type meets the requirements for creating a proxy.
        //
        // 返回结果:
        //     The entity instance, which may be a proxy.
        TEntity Create();
        //
        // 摘要:
        //     Creates a new instance of an entity for the type of this set or for a type derived
        //     from the type of this set. Note that this instance is NOT added or attached to
        //     the set. The instance returned will be a proxy if the underlying context is configured
        //     to create proxies and the entity type meets the requirements for creating a proxy.
        //
        // 类型参数:
        //   TDerivedEntity:
        //     The type of entity to create.
        //
        // 返回结果:
        //     The entity instance, which may be a proxy.
        TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity;
        //
        // 摘要:
        //     Finds an entity with the given primary key values. If an entity with the given
        //     primary key values exists in the context, then it is returned immediately without
        //     making a request to the store. Otherwise, a request is made to the store for
        //     an entity with the given primary key values and this entity, if found, is attached
        //     to the context and returned. If no entity is found in the context or the store,
        //     then null is returned.
        //
        // 参数:
        //   keyValues:
        //     The values of the primary key for the entity to be found.
        //
        // 返回结果:
        //     The entity found, or null.
        //
        // 备注:
        //     The ordering of composite key values is as defined in the EDM, which is in turn
        //     as defined in the designer, by the Code First fluent API, or by the DataMember
        //     attribute.
        TEntity Find(params object[] keyValues);
        //
        // 摘要:
        //     Marks the given entity as Deleted such that it will be deleted from the database
        //     when SaveChanges is called. Note that the entity must exist in the context in
        //     some other state before this method is called.
        //
        // 参数:
        //   entity:
        //     The entity to remove.
        //
        // 返回结果:
        //     The entity.
        //
        // 备注:
        //     Note that if the entity exists in the context in the Added state, then this method
        //     will cause it to be detached from the context. This is because an Added entity
        //     is assumed not to exist in the database such that trying to delete it does not
        //     make sense.
        TEntity Remove(TEntity entity);
    }
}
