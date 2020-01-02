import os
import sys
from PIL import Image
import math
import xml


class Rectangle:
    
    def __init__(self, x, y, width, height):
        self.x = x
        self.y = y
        self.width = width
        self.height = height
    
    def __repr__(self):
        return '{%s, %s, %s, %s}' % (self.x, self.y, self.width, self.height)

    def hasPoint(self, x, y):
        if x >= self.x and self.x + self.width >= x:
            if y >= self.y and self.y + self.height >= y:
                return True
        return False


def hasAlpha(im, x, y):
    if x >= im.size[0] or y >= im.size[1]:
        return False
    r,g,b,a = im.getpixel((x,y))
    return a != 0


def hasAdjacentPixels(visitedPixels, im, x, y):

    #Top Left
    if hasAlpha(im, x-1, y+1) and (x-1, y+1) not in visitedPixels:
        visitedPixels.add((x-1,y+1))
        hasAdjacentPixels(visitedPixels, im, x-1, y+1)
    #Top Middle
    if hasAlpha(im, x, y+1) and (x, y+1) not in visitedPixels:
        visitedPixels.add((x,y+1))
        hasAdjacentPixels(visitedPixels, im, x, y+1)
    #Top Right
    if hasAlpha(im, x+1, y+1) and (x+1, y+1) not in visitedPixels:
        visitedPixels.add((x+1,y+1))
        hasAdjacentPixels(visitedPixels, im, x+1, y+1)
    #Middle Left
    if hasAlpha(im, x-1, y) and (x-1, y) not in visitedPixels:
        visitedPixels.add((x-1,y))
        hasAdjacentPixels(visitedPixels, im, x-1, y)
    if hasAlpha(im, x, y) and (x, y) not in visitedPixels:
        visitedPixels.add((x,y))
        hasAdjacentPixels(visitedPixels, im, x, y)
    #Middle Right
    if hasAlpha(im, x + 1, y) and (x+1, y) not in visitedPixels:
        visitedPixels.add((x+1,y))
        hasAdjacentPixels(visitedPixels, im, x+1, y)
    #Bottom Left
    if hasAlpha(im, x-1, y-1) and (x-1, y-1) not in visitedPixels:
        visitedPixels.add((x-1,y-1))
        hasAdjacentPixels(visitedPixels, im, x-1, y-1)
    #Bottom Middle
    if hasAlpha(im, x, y-1) and (x, y-1) not in visitedPixels:
        visitedPixels.add((x,y-1))
        hasAdjacentPixels(visitedPixels, im, x, y-1)
    #Bottom RIght
    if hasAlpha(im, x + 1, y - 1) and (x+1,y-1) not in visitedPixels:
        visitedPixels.add((x+1,y-1))
        hasAdjacentPixels(visitedPixels, im, x+1, y-1)




def sortRects(elem):
    return math.floor(elem.y/42) + elem.x


def generateRects(im, spriteWidth, spriteHeight):
    width, height = im.size
    rects = []
    testSheet = Image.new('RGBA', (width, height), (0,0,0,0))
    pixels = testSheet.load()
    visitedPixels = set()
    rows = math.floor(width/spriteWidth)
    cols = math.floor(height/spriteHeight)

    for y in range(cols):
        for x in range(rows):
            rects.append(Rectangle(x*spriteWidth, y*spriteHeight, spriteWidth, spriteHeight))

    
    return rects
                

import xml.etree.ElementTree as ET

def writeFile(im, sprites):
    filename = im.filename
    data = ET.Element('sheet')
    data.attrib['img'] = filename
    rects = ET.SubElement(data, 'rects')
    i = 0
    for sprite in sprites:
        r = ET.SubElement(rects, 'rect')
        r.attrib['X'] = str(sprite.x)
        r.attrib['Y'] = str(sprite.y)
        r.attrib['Width'] = str(sprite.width)
        r.attrib['Height'] = str(sprite.height)
        r.attrib['Index'] = str(i)
        i += 1

    finalData = ET.tostring(data)
    with open("test.sheet", 'wb') as f:
        f.write(finalData)


def writeSheet(path, spriteWidth, spriteHeight):
    filename = path.split('.')[0]
    newPath = filename + '.sheet'
    im = Image.open(path)
    width, height = im.size
    rects = generateRects(im, spriteWidth, spriteHeight)
    
    writeFile(im, rects)






def readSheet(path):
    pass


if sys.argv[1] == 'r':
    readSheet(sys.argv[2])
else:
    writeSheet(sys.argv[1], int(sys.argv[2]), int(sys.argv[3]))

