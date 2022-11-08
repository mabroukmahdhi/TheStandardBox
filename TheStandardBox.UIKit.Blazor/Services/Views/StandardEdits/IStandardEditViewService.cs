// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Models.Components.ViewElements;

namespace TheStandardBox.UIKit.Blazor.Services.Views.StandardEdits
{
    public interface IStandardEditViewService<TEntity>
        where TEntity : IStandardEntity
    {
        List<IViewElement> GenerateViewElements(TEntity entity);

        ValueTask UpsertEntityAsync(TEntity entity, List<IViewElement> viewElements);
    }
}