import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { CartService } from '../services/cart.service';
import { SnackbarService } from '../services/snackbar.service';

export const emptyCartGuard: CanActivateFn = (route, state) => {
  const cartService = inject(CartService);
  const router = inject(Router);
  const snackbarService = inject(SnackbarService);

  if(!cartService.cart() || cartService.cart()?.items.length === 0) {
    snackbarService.error('Your cart is empty');
    router.navigateByUrl('/cart');
    return false;
  }
  return true;
};