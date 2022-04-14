using Project.KhoaHocs;
using Project.ThamGiaKhoaHocs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;

namespace Project.JoinCouses
{
    public class JoinCourseManager : DomainService
    {
        private readonly IJoinCourseRepository _joincourseRepository;

        public JoinCourseManager(IJoinCourseRepository joincourseRepository)
        {
            _joincourseRepository = joincourseRepository;
            //push change
        }

        public async Task<JoinCourse> CreateAsync(
            Guid IDCourse,
            Guid IDUser,
            bool HasBeenLock
            )
        {
            //var existingJoin = await _joincourseRepository.GetQueryableAsync();
            //if (existingJoin.Count() > 0)
            //{
            //    throw new Exception();
            //}

            var query = await _joincourseRepository.FindAsync(x => x.IDUser == IDUser && x.IDCourse == IDCourse);
           
            //if (query != null)
            //{
            //    throw new InvalidCastException();
            //}
            return new JoinCourse(
                GuidGenerator.Create(),
                IDCourse,
                HasBeenLock,
                IDUser
                
            );
        }

       
    }
}