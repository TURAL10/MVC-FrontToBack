﻿using HomeTaskkMVC4.Models;

namespace HomeTaskkMVC4.Helpers
{
    public class Pagination<T>
    {
        public  List<T> Items { get; set; }

        public int PageCount { get; set; }
        public int PageSize { get; set; }

        public Pagination(List<T> items,int pageCount,int pageSize) 
        {
            Items = items;
            PageCount = pageCount;
            PageSize = pageSize;

        }

    }
}
