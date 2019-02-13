import { CoinModel } from "./coin.model";

export interface CoinsInMachineModel {
    id: number;
    count: number;
    cointId: number;
    coin: CoinModel;
}
