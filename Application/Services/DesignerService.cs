using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class DesignerService : IDesignerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITranslationService _translationService;

        public DesignerService(AppDbContext context, IMapper mapper, ITranslationService translationService)
        {
            _context = context;
            _mapper = mapper;
            _translationService = translationService;
        }

        private async Task<Dictionary<string, string>> TranslateName(string name)
        {
            var targetLangs = new List<string> { "en", "ru", "ar" };
            return await _translationService.TranslateTextAsync(name, targetLangs);
        }

        public async Task<IEnumerable<DesignerGetDto>> GetAllAsync()
        {
            var designers = await _context.Designers.ToListAsync();
            return _mapper.Map<IEnumerable<DesignerGetDto>>(designers);
        }

        public async Task<DesignerGetDto> GetByIdAsync(Guid id)
        {
            var designer = await _context.Designers.FindAsync(id);
            return designer == null ? null : _mapper.Map<DesignerGetDto>(designer);
        }

        public async Task<DesignerGetDto> CreateAsync(DesignerPostDto dto, string uploadsFolderPath, string worksFolderPath)
        {
            // 🔵 Tərcümə et
            var nameTrans = await TranslateName(dto.Name);

            // Yükləmə qovluqları
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            if (!Directory.Exists(worksFolderPath))
                Directory.CreateDirectory(worksFolderPath);

            // 🔵 Dizaynerin öz şəkli
            var mainFileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
            var mainPath = Path.Combine(uploadsFolderPath, mainFileName);

            using (var stream = new FileStream(mainPath, FileMode.Create))
                await dto.ImageFile.CopyToAsync(stream);

            // 🔵 İş şəkilləri
            var worksImages = new List<string>();

            if (dto.WorksImageFiles != null)
            {
                foreach (var file in dto.WorksImageFiles)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(worksFolderPath, fileName);

                    using var s = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(s);

                    worksImages.Add($"/uploads/designers/works/{fileName}");
                }
            }

            var designer = new Designer
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Name_en = nameTrans.GetValueOrDefault("en"),
                Name_ru = nameTrans.GetValueOrDefault("ru"),
                Name_ar = nameTrans.GetValueOrDefault("ar"),
                ImageUrl = $"/uploads/designers/{mainFileName}",
                WorksImage = worksImages
            };

            await _context.Designers.AddAsync(designer);
            await _context.SaveChangesAsync();

            return _mapper.Map<DesignerGetDto>(designer);
        }

        public async Task<DesignerGetDto> UpdateAsync(Guid id, DesignerPutDto dto, string uploadsFolderPath, string worksFolderPath)
        {
            var designer = await _context.Designers.FindAsync(id);
            if (designer == null) return null;

            var nameTrans = await TranslateName(dto.Name);

            // 🔵 Əsas şəkil yenilənirsə
            if (dto.ImageFile != null)
            {
                var newFile = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
                var path = Path.Combine(uploadsFolderPath, newFile);

                using var stream = new FileStream(path, FileMode.Create);
                await dto.ImageFile.CopyToAsync(stream);

                designer.ImageUrl = $"/uploads/designers/{newFile}";
            }

            // 🔵 Yeni iş şəkilləri əlavə olunursa
            if (dto.WorksImageFiles != null && dto.WorksImageFiles.Any())
            {
                foreach (var file in dto.WorksImageFiles)
                {
                    var newFile = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var path = Path.Combine(worksFolderPath, newFile);

                    using var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    designer.WorksImage.Add($"/uploads/designers/works/{newFile}");
                }
            }

            // 🔵 Mətn
            designer.Name = dto.Name;
            designer.Name_en = nameTrans.GetValueOrDefault("en");
            designer.Name_ru = nameTrans.GetValueOrDefault("ru");
            designer.Name_ar = nameTrans.GetValueOrDefault("ar");

            await _context.SaveChangesAsync();

            return _mapper.Map<DesignerGetDto>(designer);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var designer = await _context.Designers.FindAsync(id);
            if (designer == null) return false;

            _context.Designers.Remove(designer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
