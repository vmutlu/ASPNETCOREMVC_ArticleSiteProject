using AutoMapper;
using HappyBlog.Entities.Concrete;
using HappyBlog.Entities.DTOs;
using HappyBlog.Shared.Utilities.Extensions;
using HappyBlog.Shared.Utilities.Results.ComplexTypes;
using HappyBlog.Web.Areas.Admin.Models;
using HappyBlog.Web.Helpers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HappyBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;
        public UserController(UserManager<User> userManager, IWebHostEnvironment env, IMapper mapper, SignInManager<User> signInManager, IImageHelper imageHelper)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync().ConfigureAwait(false);
            return View(new UserListDTO
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }

        [HttpGet]
        public ViewResult AccessDenied() => View();

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false); ;

            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [HttpGet]
        public IActionResult UserLogin() => View();

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDTO.Password, userLoginDTO.RememberMe, false);//hesap kilitlenme istemiyorum false verdim.

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-Mail adresi veya şifre hatalı tekrar deneyiniz.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-Mail adresi veya şifre hatalı tekrar deneyiniz.");
                    return View();
                }
            }

            else
                return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync().ConfigureAwait(false); ;

            var userListDTO = JsonSerializer.Serialize(new UserListDTO
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            },
            new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            return Json(userListDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()).ConfigureAwait(false); ;
            var result = await _userManager.DeleteAsync(user).ConfigureAwait(false); ;

            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDTO
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{user.UserName} adlı kullanıcı başarıyla silinmiştir",
                    User = user
                });

                return Json(deletedUser);
            }

            else
            {
                string errorMessages = string.Empty;
                foreach (var error in result.Errors)
                    errorMessages = $"*{error.Description} \n";

                var deletedErrorMessages = JsonSerializer.Serialize(new UserDTO
                {
                    ResultStatus = ResultStatus.Error,
                    Message = $"{user.UserName} adlı kullanıcı silinirken hata meydana geld. ! \n {errorMessages}",
                    User = user
                });

                return Json(deletedErrorMessages);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId).ConfigureAwait(false); ;
            var userUpdateDTO = _mapper.Map<UserUpdateDTO>(user);

            return PartialView("_UserUpdatePartial", userUpdateDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDTO userUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDTO.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDTO.PictureFile != null)
                {
                    var uploadedImageDTOResult = await _imageHelper.UploadedUserImage(userUpdateDTO.UserName, userUpdateDTO.PictureFile).ConfigureAwait(false); ;
                    userUpdateDTO.Picture = uploadedImageDTOResult.ResultStatus == ResultStatus.Success ? uploadedImageDTOResult.Data.FullName : "userImages/default.png";

                    if (oldUserPicture != "userImages/default.png")
                        isNewPictureUploaded = true;
                }

                var updatedUser = _mapper.Map<UserUpdateDTO, User>(userUpdateDTO, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser).ConfigureAwait(false); ;

                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                        _imageHelper.Delete(oldUserPicture);

                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDTO = new UserDTO
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.",
                            User = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDTO)
                    });

                    return Json(userUpdateViewModel);
                }

                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDTO = userUpdateDTO,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDTO)
                    });

                    return Json(userUpdateViewModel);
                }
            }

            else
            {
                var userUpdateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                {
                    UserUpdateDTO = userUpdateDTO,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDTO)
                });

                return Json(userUpdateErrorViewModel);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add() => PartialView("_UserAddPartial");

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDTO userAddDTO)
        {
            if (ModelState.IsValid)
            {
                var uploadedImageDTOResult = await _imageHelper.UploadedUserImage(userAddDTO.UserName, userAddDTO.PictureFile).ConfigureAwait(false); ;
                userAddDTO.Picture = uploadedImageDTOResult.ResultStatus == ResultStatus.Success ? uploadedImageDTOResult.Data.FullName : "userImages/default.png";

                var user = _mapper.Map<User>(userAddDTO);
                var result = await _userManager.CreateAsync(user, userAddDTO.Password).ConfigureAwait(false); ;

                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDTO = new UserDTO
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{user.UserName} adlı kullanıcı adına sahip, kullanıcı başarıyla eklenmiştir.",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDTO)
                    });

                    return Json(userAddAjaxModel);
                }

                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDTO = userAddDTO,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDTO)
                    });

                    return Json(userAddAjaxErrorModel);
                }
            }

            var userAddAjaxModelStateError = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDTO = userAddDTO,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDTO)
            });

            return Json(userAddAjaxModelStateError);
        }

        [Authorize]
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false); ;
            var updateDTO = _mapper.Map<UserUpdateDTO>(user);
            return View(updateDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDTO userUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.GetUserAsync(HttpContext.User);
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDTO.PictureFile != null)
                {
                    var uploadedImageDTOResult = await _imageHelper.UploadedUserImage(userUpdateDTO.UserName, userUpdateDTO.PictureFile).ConfigureAwait(false); ;
                    userUpdateDTO.Picture = uploadedImageDTOResult.ResultStatus == ResultStatus.Success ? uploadedImageDTOResult.Data.FullName : "userImages/default.png";

                    if (oldUserPicture != "userImages/default.png")
                        isNewPictureUploaded = true;
                }

                var updatedUser = _mapper.Map<UserUpdateDTO, User>(userUpdateDTO, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser).ConfigureAwait(false); ;

                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                        _imageHelper.Delete(oldUserPicture);

                    TempData.Add("SuccessMessage", $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.");
                    return View(userUpdateDTO);
                }

                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    return View(userUpdateDTO);
                }
            }

            else
                return View(userUpdateDTO);
        }

        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDTO userPasswordChangeDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false); ;
                if (user != null)
                {
                    var isVerify = await _userManager.CheckPasswordAsync(user, userPasswordChangeDTO.CurrentPassword).ConfigureAwait(false); ;

                    if (isVerify)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDTO.CurrentPassword, userPasswordChangeDTO.NeWPassword).ConfigureAwait(false); ;
                        if (result.Succeeded)
                        {
                            await _userManager.UpdateSecurityStampAsync(user).ConfigureAwait(false); ;
                            await _signInManager.SignOutAsync().ConfigureAwait(false); ;

                            await _signInManager.PasswordSignInAsync(user, userPasswordChangeDTO.NeWPassword, true, false); // şifre değiştirme başarılıysa çıkış yapıp tekrar giriş yaptırdım.

                            TempData.Add("SuccessMessage", "Şifreniz başarıyla güncellenmiştir.");
                            return View();
                        }

                        else
                        {
                            foreach (var error in result.Errors)
                                ModelState.AddModelError("", error.Description);

                            return View(userPasswordChangeDTO);
                        }
                    }

                    else
                    {
                        ModelState.AddModelError("", "Lütfen eski şifrenizi doğru girdiginizden emin olunuz !");

                        return View(userPasswordChangeDTO);
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Böyle bir kullanıcı kaydı bulunamadı.");

                    return View(userPasswordChangeDTO);
                }
            }

            else
                return View(userPasswordChangeDTO);
        }
    }
}
