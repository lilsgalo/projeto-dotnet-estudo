using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuProjeto.Business.DTOs
{
    public interface IPagedList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PagingInformation PagingInformation { get; set; }
    }
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList(IEnumerable<T> items, PagingInformation pagingInformation)
        {
            PagingInformation = pagingInformation;
            Items = items;
        }

        public IEnumerable<T> Items { get; set; }
        public PagingInformation PagingInformation { get; set; }
        
        public static async Task<IPagedList<T>> ToPagedList(IQueryable<T> source, int currentPage, int pageSize)
        {
            var totalCount = source.Count();
            currentPage = currentPage > 0 ? currentPage : 1;
            var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            
            return new PagedList<T>(items, new PagingInformation(totalCount, pageSize, currentPage));
        }
    }

    public class PagingInformation
    {
        public PagingInformation(int totalCount, int pageSize, int currentPage)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }

}
