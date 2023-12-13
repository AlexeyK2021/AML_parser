if __name__ == '__main__':
    text = input().replace("\n", " ")
    new_text = list()
    currtab = 0
    for i in text:
        if i == "[":
            currtab += 1
            new_text.append("[\n" + "\t" * currtab)
        elif i == " ":
            new_text.append("\n" + "\t" * currtab)
        elif i == "]":
            currtab -= 1
            new_text.append("\n" + "\t" * currtab + "]")
        else:
            new_text += i
    print("".join(new_text))
