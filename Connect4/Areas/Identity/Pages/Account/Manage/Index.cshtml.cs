﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Connect4.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = dbContext;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Telefone")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Nome")]
            public String Nome { get; set; }

            [Required]
            [Display(Name = "Data de nascimento")]
            [DataType(DataType.Date)]
            public DateTime Nascimento { get; set; }

            [Required]
            [RegularExpression(@"^[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}$", ErrorMessage = "Seu CPF está fora do padrão.")]
            [Display(Name = "CPF")]
            [MaxLength(14)]
            public string CPF { get; set; }

            [Required]
            [RegularExpression(@"^[0-9]{5}-[0-9]{3}$", ErrorMessage = "Seu CEP está fora do padrão.")]
            [Display(Name = "CEP")]
            [MaxLength(9)]
            public string CEP { get; set; }

            [Required]
            [Display(Name = "Endereço")]
            public string Endereco { get; set; }

            [Required]
            [Display(Name = "Número")]
            public string NumeroCasa { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nome = user.Nome,
                Nascimento = user.Nascimento,
                CPF = user.CPF,
                CEP = user.CEP,
                Endereco = user.Endereco,
                NumeroCasa = user.NumeroCasa
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            Boolean cpfValido = user.ValidaCPF(Input.CPF);

            if (!cpfValido)
            {
                await LoadAsync(user);
                ModelState.AddModelError("CPF", "Insira um CPF válido.");
                return Page();
            }

            var cpfAlreadyRegistered = _context.ApplicationUser.Where(u => u.CPF == Input.CPF).FirstOrDefault();

            if (cpfAlreadyRegistered != null && cpfAlreadyRegistered.Id != user.Id)
            {
                await LoadAsync(user);
                ModelState.AddModelError("CPF", "Já existe alguem cadastrado com esse CPF.");
                return Page();
            }

            var idade = (DateTime.Now - Input.Nascimento).TotalDays / 365;

            if (idade < 18 || idade > 120)
            {
                await LoadAsync(user);
                ModelState.AddModelError("Nascimento", "Sua idade deverá ser entre 18 e 120 anos");
                return Page();
            }

            user.Nascimento = Input.Nascimento;
            user.Nome = Input.Nome;
            user.CPF = Input.CPF;
            user.CEP = Input.CEP;
            user.Endereco = Input.Endereco;
            user.NumeroCasa = Input.NumeroCasa;

            var update = await _userManager.UpdateAsync(user);
            if (!update.Succeeded)
            {
                ModelState.AddModelError("Erro", $"Erro ao atualizar o usuário com o ID '{user.Id}'.");
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Dados atualizados com sucesso.";
            return RedirectToPage();
        }
    }
}
