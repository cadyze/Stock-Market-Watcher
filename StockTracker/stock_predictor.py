import numpy as np
import pandas as pd
from sklearn.preprocessing import MinMaxScaler
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import LSTM, Dense

time_step = 20
# Assumes the closing_prices and volumes are of the same shape and are of length 30
def predict_closing_price(stockPath, closing_prices, volumes):
    model = create_model(stockPath)
    data, price_scaler = setup_data(closing_prices, volumes)
    prediction = model.predict(data)
    return price_scaler.inverse_transform(prediction)

def setup_data(closing_prices, volumes):
    price_scaler = MinMaxScaler(feature_range=(0, 1))
    volume_scaler = MinMaxScaler(feature_range=(0, 1))
    scaled_closings = price_scaler.fit_transform(closing_prices.reshape(-1, 1))
    scaled_volumes = volume_scaler.fit_transform(volumes.reshape(-1, 1))

    scaled_data = np.hstack((scaled_closings, scaled_volumes))
    X = scaled_data.reshape(1, time_step, 2)
    return X, price_scaler

# Creates a LSTM model based off the data in provided .csv
def create_model(stockPath):
    stock_data = pd.read_csv("stocks\A.csv")

    # Ensure 'date' column is in datetime format if not already parsed by the date_parser
    stock_data['Date'] = pd.to_datetime(stock_data['Date'])

    last_date = stock_data['Date'].max()
    start_date = last_date - pd.DateOffset(years=2)
    filtered_data = stock_data[(stock_data['Date'] >= start_date) & (stock_data['Date'] <= last_date)]

    adj_closing_prices = filtered_data['Adj Close'].values.reshape(-1,1)
    volumes = filtered_data['Volume'].values.reshape(-1, 1)

    # Normalize the data
    price_scaler = MinMaxScaler(feature_range=(0, 1))
    volume_scaler = MinMaxScaler(feature_range=(0, 1))
    adj_closing_prices_scaled = price_scaler.fit_transform(adj_closing_prices)
    volumes_scaled = volume_scaler.fit_transform(volumes)

    scaled_data = np.hstack((adj_closing_prices_scaled, volumes_scaled))

    # Create sequences for LSTM
    def create_dataset(dataset, time_step=22):
        dataX, dataY = [], []
        for i in range(len(dataset) - time_step):
            a = dataset[i:(i + time_step)]
            dataX.append(a)
            dataY.append(dataset[i + time_step, 0])
        return np.array(dataX), np.array(dataY)

    X, y = create_dataset(scaled_data, time_step)
    X = X.reshape(X.shape[0], time_step, 2)

    model = Sequential([
        LSTM(50, return_sequences=True, input_shape=(time_step, 2)),
        LSTM(50, return_sequences=False),
        Dense(1)
    ])

    model.compile(loss='mean_squared_error', optimizer='adam')

    model.fit(X, y, epochs=15, batch_size=1, verbose=2)
    return model