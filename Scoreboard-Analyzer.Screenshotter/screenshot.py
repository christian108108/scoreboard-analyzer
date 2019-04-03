#!/usr/bin/python3

import sys
from datetime import *
from selenium import webdriver
from selenium.webdriver.firefox.options import Options

# create headless instance of firefox
options = Options()
options.headless = True
driver = webdriver.Firefox(options=options)

# get command line arg as url
url = str(sys.argv[1])

# take screenshot of the given url
driver.get(url)

# generate unique filename based off of current datetime
current_datetime = datetime.utcnow().replace(microsecond=0).isoformat()

# replace the : with . so it can save properly
current_datetime = current_datetime.replace(":", ".")

# save screenshot in the screenshots directory
driver.save_screenshot(f"/home/christian/screenshots/{current_datetime}.png")
print(f"Screenshot saved @ {current_datetime}")

# quit driver
driver.quit()
