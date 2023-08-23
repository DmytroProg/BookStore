using BookStoreCore.ViewModels;
using System;
using Xunit;

namespace ContactsTests
{
    public class UnitTest1
    {
        private void SetAcceptableBookBiewModel(BookViewModel bookViewModel)
        {
            bookViewModel.Title = "Title";
            bookViewModel.Price = 9.99m;
            bookViewModel.Publisher = "Publisher";
            bookViewModel.Author = bookViewModel.Authors[0];
            bookViewModel.Value = 7.99m;
            bookViewModel.Genres[0].IsSelected = true;
            bookViewModel.ImagePath = "C:\\img.png";
            bookViewModel.PageCount = 100;
            bookViewModel.PublishYear = 1999;
            bookViewModel.Part = 2;
        }

        [Fact]
        public void TitleValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);
            var bookViewModel2 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);
            SetAcceptableBookBiewModel(bookViewModel2);

            bookViewModel1.Title = "";
            bookViewModel2.Title = "     ";

            bool result1, result2;
            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            try
            {
                bookViewModel2.Validate();
                result2 = false;
            }
            catch (ArgumentException)
            {
                result2 = true;
            }
            catch (Exception)
            {
                result2 = false;
            }

            Assert.True(result1 && result2);
        }

        [Fact]
        public void PublisherValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);
            var bookViewModel2 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);
            SetAcceptableBookBiewModel(bookViewModel2);

            bookViewModel1.Publisher = "";
            bookViewModel2.Publisher = "     ";

            bool result1, result2;
            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            try
            {
                bookViewModel2.Validate();
                result2 = false;
            }
            catch (ArgumentException)
            {
                result2 = true;
            }
            catch (Exception)
            {
                result2 = false;
            }

            Assert.True(result1 && result2);
        }

        [Fact]
        public void ImagePathValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);
            var bookViewModel2 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);
            SetAcceptableBookBiewModel(bookViewModel2);

            bookViewModel1.ImagePath = "";
            bookViewModel2.ImagePath = "     ";

            bool result1, result2;
            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            try
            {
                bookViewModel2.Validate();
                result2 = false;
            }
            catch (ArgumentException)
            {
                result2 = true;
            }
            catch (Exception)
            {
                result2 = false;
            }

            Assert.True(result1 && result2);
        }

        [Fact]
        public void PriceValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);
            var bookViewModel2 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);
            SetAcceptableBookBiewModel(bookViewModel2);

            bookViewModel1.Price = -23.0m;
            bookViewModel2.Price = 0.0m;

            bool result1, result2;
            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            try
            {
                bookViewModel2.Validate();
                result2 = false;
            }
            catch (ArgumentException)
            {
                result2 = true;
            }
            catch (Exception)
            {
                result2 = false;
            }

            Assert.True(result1 && result2);
        }

        [Fact]
        public void ValueValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);
            var bookViewModel2 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);
            SetAcceptableBookBiewModel(bookViewModel2);

            bookViewModel1.Value = -23.0m;
            bookViewModel2.Value = 0.0m;

            bool result1, result2;
            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            try
            {
                bookViewModel2.Validate();
                result2 = false;
            }
            catch (ArgumentException)
            {
                result2 = true;
            }
            catch (Exception)
            {
                result2 = false;
            }

            Assert.True(result1 && result2);
        }

        [Fact]
        public void PublisherYearValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);
            var bookViewModel2 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);
            SetAcceptableBookBiewModel(bookViewModel2);

            bookViewModel1.PublishYear = 0;
            bookViewModel2.PublishYear = -1459;

            bool result1, result2;
            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            try
            {
                bookViewModel2.Validate();
                result2 = false;
            }
            catch (ArgumentException)
            {
                result2 = true;
            }
            catch (Exception)
            {
                result2 = false;
            }

            Assert.True(result1 && result2);
        }

        [Fact]
        public void PageCountValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);
            var bookViewModel2 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);
            SetAcceptableBookBiewModel(bookViewModel2);

            bookViewModel1.PageCount = -23;
            bookViewModel2.PageCount = 0;

            bool result1, result2;
            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            try
            {
                bookViewModel2.Validate();
                result2 = false;
            }
            catch (ArgumentException)
            {
                result2 = true;
            }
            catch (Exception)
            {
                result2 = false;
            }

            Assert.True(result1 && result2);
        }

        [Fact]
        public void PartValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);
            var bookViewModel2 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);
            SetAcceptableBookBiewModel(bookViewModel2);

            bookViewModel1.Part = -1;
            bookViewModel2.Part = 0;

            bool result1, result2;
            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            try
            {
                bookViewModel2.Validate();
                result2 = false;
            }
            catch (ArgumentException)
            {
                result2 = true;
            }
            catch (Exception)
            {
                result2 = false;
            }

            Assert.True(result1 && result2);
        }

        [Fact]
        public void AuthorValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);
            var bookViewModel2 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);
            SetAcceptableBookBiewModel(bookViewModel2);

            bookViewModel1.Author = null;
            bookViewModel2.Author = new BusinessLogicLayer.Models.Author();

            bool result1, result2;
            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            try
            {
                bookViewModel2.Validate();
                result2 = false;
            }
            catch (ArgumentException)
            {
                result2 = true;
            }
            catch (Exception)
            {
                result2 = false;
            }

            Assert.True(result1 && result2);
        }

        [Fact]
        public void GenreValidationTest()
        {
            //Asign
            var bookViewModel1 = new BookViewModel(null);

            //Act
            SetAcceptableBookBiewModel(bookViewModel1);

            bookViewModel1.Genres.ForEach(item => item.IsSelected = false);
            bool result1;

            //Assert
            try
            {
                bookViewModel1.Validate();
                result1 = false;
            }
            catch (ArgumentException)
            {
                result1 = true;
            }
            catch (Exception)
            {
                result1 = false;
            }

            Assert.True(result1);
        }
    }
}