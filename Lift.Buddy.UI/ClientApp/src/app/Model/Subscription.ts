export enum SubscriptionType {
  Free,
  Timespan,
  Lifetime
}

export class Subscription {
  public trainerId: string = '';
  public athleteId: string = '';
  public subscriptionType: SubscriptionType = SubscriptionType.Free;
  public expiration: Date | undefined;
}