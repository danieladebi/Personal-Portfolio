from data_processing import *
from neural_network import *

import tensorflow as tf
import numpy as np
import selenium

import matplotlib.pyplot as plt
from matplotlib import style
from statistics import mean

style.use('ggplot')

params = {
    "batch_size": 128,
    "epochs": 20,
    "lr": 0.00010000,
    "time_steps": 60
}

if __name__ == '__main__':
   # compile_data()

    with open("Ticker_List/sp500tickers.pickle", "rb") as f:
        tickers = pickle.load(f)

    accuracies = []
    print(train_and_run_model("AAPL"))

