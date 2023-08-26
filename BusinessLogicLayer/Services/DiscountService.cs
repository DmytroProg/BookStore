using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class DiscountService : IService<Discount>
    {
        private IRepository<DiscountInfo> _discountrRepository;
        private BookService _bookService;

        public DiscountService(string connectionString)
        {
            this._discountrRepository = new DiscountRepository(connectionString);
            this._bookService = new BookService(connectionString);
        }

        private Discount TranslateToDiscountModel(DiscountInfo discount)
        {
            return new Discount()
            {
                Name = discount.Name,
                Percents = discount.Percents,
                Id = discount.Id,
            };
        }

        public DiscountInfo TranslateToDiscountInfo(Discount discount) {
            return new DiscountInfo()
            {
                Name = discount.Name,
                Percents = discount.Percents,
            };
        }

        public void Add(Discount value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            this._discountrRepository.Add(TranslateToDiscountInfo(value));
        }

        public Discount FindOne(int id)
        {
            return GetAll().First(x => x.Id == id);
        }

        public IEnumerable<Discount> GetAll()
        {
            return this._discountrRepository.GetAll().Select(x => TranslateToDiscountModel(x));
        }

        public void Remove(Discount value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            this._discountrRepository.Remove(TranslateToDiscountInfo(value));
        }

        public void Update(Discount value)
        {
            throw new NotImplementedException();
        }
    }
}
