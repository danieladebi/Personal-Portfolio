import os
import requests
import pickle
import bs4 as bs
import pandas as pd

from datetime import datetime as dt
import yfinance as yf

"""
Save and Load Stock Data
"""

TICKER_DATA_PATH = "Ticker_List/sp500tickers.pickle"


def save_tickers():
    '''
    Save Ticker data
    :return: list of tickers
    '''
    resp = requests.get('https://en.wikipedia.org/wiki/List_of_S%26P_500_companies')
    soup = bs.BeautifulSoup(resp.text, 'lxml')
    table = soup.find('table', {'class': 'wikitable sortable'})
    tickers = []
    for row in table.findAll('tr')[1:]:
        ticker = row.findAll('td')[0].text.replace(".", "-").strip()
        tickers.append(ticker)
    if not os.path.exists("./Ticker_List"):
        os.makedirs("./Ticker_List")
    with open(TICKER_DATA_PATH, "wb") as f:
        pickle.dump(tickers, f)
    return tickers


def get_stock_data(reload=False, update_data=False, start=dt(2010, 1, 1), end=dt.now(), tickers_to_update=set()):
    '''
    Gathers stock data from yahoo
    '''
    stock_folder_name = "Individual_Stock_Data"
    if reload:
        tickers = save_tickers()
    else:
        try:
            with open(TICKER_DATA_PATH, "rb") as f:
                tickers = pickle.load(f)
        except FileNotFoundError:
            tickers = save_tickers()

    if not os.path.exists(stock_folder_name):
        os.mkdir(stock_folder_name)

    for ticker in tickers:
        # just in case connection breaks, progress is still saved
        if (not os.path.exists(f'{stock_folder_name}/{ticker}.csv')) or update_data or ticker in tickers_to_update:
            df = yf.download(ticker, start, end)
            df.reset_index(inplace=True)
            df.set_index("Date", inplace=True)
            df.to_csv(f"{stock_folder_name}/{ticker}.csv")
        else:
            print(f'Already have {ticker}')


def compile_data():
    '''
    Compiles data in a processable format.
    :return:
    '''
    with open(TICKER_DATA_PATH, 'rb') as f:
        tickers = pickle.load(f)

    main_df = pd.DataFrame()
    for count, ticker in enumerate(tickers):
        stock_folder_name = f'Individual_Stock_Data/{ticker}.csv'
        df = pd.read_csv(f'{stock_folder_name}')
        df.set_index('Date', inplace=True)

        df.rename(columns={'Adj Close': ticker}, inplace=True)
        df.drop(['Open', 'High', 'Low', 'Close', 'Volume'], 1, inplace=True)

        if main_df.empty:
            main_df = df
        else:
            main_df = main_df.join(df, how='outer')

        if count%10 == 0:
            print(count)
    print(main_df.head())
    main_df.to_csv(f'sp500_adj_close_prices.csv')


def process_data_for_labels(ticker, hm_days=7):
    """
    Gives percent change values
    :param ticker: specific company to be trained
    :param hm_days: how many days we want to predict into the future
    :return: tickers used as well as the dataframe with our specific ticker
    """
    df = pd.read_csv('sp500_adj_close_prices.csv', index_col=0)
    tickers = df.columns.values.tolist()
    df.fillna(0, inplace=True)

    for i in range(1, hm_days+1):
        df[f'{ticker}_{i}d'] = (df[ticker].shift(-i) - df[ticker]) / df[ticker]

    df.fillna(0, inplace=True)
    return tickers, df, hm_days

