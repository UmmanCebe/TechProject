,using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCareer.Models.Dtos.Category.ResponseDto;

public record  CategoryDto(int Id, string Name,DateTime CreatedDate, DateTime UpdatedDate);

