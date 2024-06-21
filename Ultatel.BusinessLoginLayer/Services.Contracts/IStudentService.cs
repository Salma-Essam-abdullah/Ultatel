﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Helpers;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface IStudentService
    {
        Task<Response> AddStudentAsync(StudentDto model);
        Task<Response> UpdateStudentAsync(int studentId, UpdateStudentDto model);
        Task<Response> DeleteStudentAsync(int studentId);
        Task<StudentDto> ShowStudentAsync(int studentId);
        Task<Pagination<StudentDto>> ShowAllStudentsAsync(int pageIndex, int pageSize);

        Task<Pagination<StudentDto>> ShowAllStudentsByUserId(string userId, int pageIndex, int pageSize);

        Task<IEnumerable<StudentDto>> SearchStudentsAsync(StudentSearchDto searchDto);

        Task<IEnumerable<StudentLogsDto>> ShowStudentLogs(int studentId);


    }
}