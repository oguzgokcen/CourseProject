// src/components/LanguageDropdown.js
import React, { useState } from 'react';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormControl from '@mui/material/FormControl';
import FormLabel from '@mui/material/FormLabel';

const LanguageDropdown = ({ selectedLanguage, onLanguageSelect }) => {
    const Languages = {
        TURKISH: 0,
        ENGLISH: 1,
        GERMAN: 2,
        SPANISH: 3,
        FRENCH: 4
    };

    const handleLanguageChange = (event) => {
        const language = event.target.value;

        onLanguageSelect(language);
    };

    return (
        <FormControl component="fieldset">
            <h5>Language</h5>
            <RadioGroup
                value={selectedLanguage}
                onChange={handleLanguageChange}
            >
                <FormControlLabel
                    value={Languages.TURKISH}
                    control={<Radio />}
                    label="Türkçe"
                />
                <FormControlLabel
                    value={Languages.ENGLISH}
                    control={<Radio />}
                    label="English"
                />
                <FormControlLabel
                    value={Languages.GERMAN}
                    control={<Radio />}
                    label="Deutsch"
                />
                <FormControlLabel
                    value={Languages.SPANISH}
                    control={<Radio />}
                    label="Español"
                />
                <FormControlLabel
                    value={Languages.FRENCH}
                    control={<Radio />}
                    label="Français"
                />
            </RadioGroup>
        </FormControl>
    );
};

export default LanguageDropdown;