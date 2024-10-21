using ApplicationB.ContractsMAFP;
using AutoMapper;
using DTOsB.Product;
using DTOsB.Shared;
using ModelsB.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.ServiceB
{
    public class ProductServiceB : IproductServiceB
    {
        private readonly IproductReposirtoryB productRepository;
        private readonly IMapper mapper;

        public ProductServiceB(IproductReposirtoryB _productRepository, IMapper _mapper)
        {
            productRepository = _productRepository;
            mapper = _mapper;
        }

        public Task<ResultViewB<ProductB>> CreateAsync(ProductB entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResultViewB<ProductB>> DeleteAsync(ProductB entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductB>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductB> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetProductDTO>> SearchByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ResultViewB<ProductB>> UpdateAsync(ProductB entity)
        {
            throw new NotImplementedException();
        }

        //public Task<ResultViewMAFP<CreateOrUpdateProductDTO>> CreateAsync(CreateOrUpdateProductDTO entity)
        //{
        //    ResultViewMAFP<CreateOrUpdateBookDTO> result = new ResultViewMAFP<CreateOrUpdateBookDTO>();

        //    try
        //    {
        //        // Null check for entity
        //        if (entity == null)
        //        {
        //            result = new ResultView<CreateOrUpdateBookDTO>()
        //            {
        //                Entity = null,
        //                IsSuccess = false,
        //                Msg = "Invalid input: Book data is null."
        //            };
        //            return result;
        //        }

        //        // Ensure case-insensitive title comparison and optimized existence check
        //        bool exists = await bookRepository
        //            .GetAllAsync()
        //            .AnyAsync(b => b.Title.ToLower() == entity.Title.ToLower());

        //        if (exists)
        //        {
        //            result = new()
        //            {
        //                Entity = null,
        //                IsSuccess = false,
        //                Msg = "Book title already exists."
        //            };
        //            return result;
        //        }

        //        // Map DTO to entity
        //        var book = mapper.Map<Book>(entity);
        //        var successEntity = await bookRepository.CreateAsync(book);

        //        // Map the entity back to the DTO to return
        //        var returnedBook = mapper.Map<CreateOrUpdateBookDTO>(successEntity);

        //        result = new ResultView<CreateOrUpdateBookDTO>()
        //        {
        //            Entity = returnedBook,
        //            IsSuccess = true,
        //            Msg = "Book added successfully."
        //        };

        //        return result;
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        // Handle database-specific errors (e.g., constraint violations)
        //        result = new()
        //        {
        //            Entity = null,
        //            IsSuccess = false,
        //            Msg = "Database error: " + dbEx.Message // Log the exact error in a real-world scenario, avoid exposing raw errors to users.
        //        };
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        // General exception handler
        //        result = new()
        //        {
        //            Entity = null,
        //            IsSuccess = false,
        //            Msg = "An unexpected error occurred: " + ex.Message // Log the exception for internal debugging.
        //        };
        //        return result;
        //    }
        //}

        //public Task<ResultViewMAFP<ProductMAFP>> CreateAsync(ProductMAFP entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResultViewMAFP<ProductMAFP>> DeleteAsync(ProductMAFP entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<ProductMAFP>> GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ProductMAFP> GetByIDAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> SaveChangesAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetProductDTO>> SearchByNameAsync(string name)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResultViewMAFP<ProductMAFP>> UpdateAsync(ProductMAFP entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
