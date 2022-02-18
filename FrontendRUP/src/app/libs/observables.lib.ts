import { Observable } from 'rxjs';

export const serial = async (...observables: Observable<any>[]): Promise<boolean> => {
  for (const observable of observables) {
    await toPromise(observable);
  }
  return true;
};

export const combine = async (...observables: Observable<any>[]): Promise<any[]> => {
  const promises = observables.map((obs) => toPromise(obs));
  return await Promise.all(promises);
};

export const toPromise = (observable: Observable<any>): Promise<any> => {
  return new Promise((resolve, reject) => {
    const subscription = observable.subscribe((response) => {
      if (response) {
        resolve(response);
        (setTimeout(() => {
          subscription.unsubscribe();
        }, 1000));
      }
    });
  });
};
