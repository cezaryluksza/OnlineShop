﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        void SaveCategory(Category category);

        Category DeleteCategory(int categoryId);

        int GetCategoryId(string categoryName);
    }
}
