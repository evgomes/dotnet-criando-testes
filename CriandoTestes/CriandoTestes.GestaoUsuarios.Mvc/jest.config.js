module.exports = {
    roots: ['<rootDir>/wwwroot/js'],
    testMatch: ['**/__tests__/**/*.+(js|jsx)', '**/?(*.)+(spec|test).+(js|jsx)'],
    transform: {
        '^.+\\.(js|jsx)$': 'babel-jest',
    },
    collectCoverage: true,
    coverageDirectory: '<rootDir>/../coverage-js',
    coverageReporters: ['html', 'text', 'lcov'],
};
